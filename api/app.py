from flask import Flask, request
from flask import jsonify
from flask import abort
from flask import make_response

import os
import math
import numpy as np
import pymongo
from pymongo import MongoClient
from VNlp.VNlp import VNlp
from data_utils.data_utils import DataUtils
from textrank.textrank import TextRank


db_host = os.environ.get('MONGO_HOST', 'localhost')
db_port = os.environ.get('MONGO_PORT', '27017')
db_user = os.environ.get('MONGO_USER', 'root')
db_pass = os.environ.get('MONGO_PASS', 'root')
db_name = os.environ.get('MONGO_NAME', 'news')

# connect database
client = MongoClient(host=db_host, port=int(db_port),
                     username=db_user, password=db_pass)
db = client.get_database(db_name)
post_collection = db.get_collection("post")
if "title_text" not in post_collection.index_information():
    post_collection.create_index([("title", pymongo.TEXT)], name="title_text")

# Load model
control = VNlp()
control.from_bin('./VNlp/model/wiki.vi.model')
utils = DataUtils()

app = Flask(__name__)
@app.route('/')
def index():
    return "Hello, World!"

# fetch category
@app.get('/api/category')
def fetch_category():
    categories = list(post_collection.distinct('category'))
    return jsonify(data=categories)

# fetch news in category
@app.get('/api/news')
def fetch_news():
    args = dict(request.args)
    category = args.get('category', None)
    search_key = args.get('search', None)
    page = int(args.get('page', 1))
    _lim = 10
    _skip = _lim * (page - 1)
    query = {}
    if category:
        query.update({"category": category})
    if search_key:
        query.update({"$text": {"$search": search_key}})
    posts = list(post_collection.find(query, {'title': 1, 'id': 1, 'image': 1, 'author': 1, '_id': 0}).skip(_skip).limit(_lim))
    return jsonify(data=posts)

def sumarization(_content, _rate):
    _data = utils.nomalize_document(_content)
    _sent_list = utils.split_sentences(_data)

    _sent_vec = []
    for i, sent in enumerate(_sent_list):
        _sent_vec.append(control.to_vector(sent.lower()))
    _sent_size = len(_sent_vec)
    cosines = np.zeros((_sent_size, _sent_size), dtype=float) 
    for i in range(_sent_size):
        for j in range(_sent_size):
            if i != j:
                cosines[i][j] = control.cosine_distance(_sent_vec[i], _sent_vec[j])
    rank = TextRank()
    rank.run(cosines)
    _rate = (100 - _rate * 100)/100
    _out_len = math.floor(len(list(rank.sorted_scores.keys())) * _rate)
    _out_key = list(rank.sorted_scores.keys())[:_out_len]
    _out_key.sort()
    _out_list = [_sent_list[i] for i in _out_key]
    return _out_list

# get news with id
@app.get('/api/news/<string:id_>')
def get_news(id_):
    compress = float(request.args.get('compress', 0.7))
    post = post_collection.find_one({"id": id_}, {'_id': 0})
    if not post:
        abort(404)
    _content = post.get("content", "")
    post.update({"summary": _content})
    try:
        _summar = sumarization(_content, compress)
        post.update({"summary": ".\n".join(_summar)})
        pass
    except:
        pass
    return jsonify(data=post)

# error handle
@app.errorhandler(404)
def not_found(error):
    print(error)
    return make_response(jsonify(error="Not found"), 404)
@app.errorhandler(500)
def server_error(error):
    print(error)
    return make_response(jsonify(error="Internal Server Error"), 500)

if __name__ == '__main__':
    app.run(debug=False, host='0.0.0.0', port=1998)
