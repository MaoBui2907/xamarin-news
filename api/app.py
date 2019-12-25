from flask import Flask
from flask import jsonify
from flask import abort
from flask import make_response

from VNlp.VNlp import VNlp
from data_utils.data_utils import DataUtils
import numpy as np
from textrank.textrank import TextRank
import math

import json

from pymongo import MongoClient
from mongo_json import MongoJSONEncoder

# read env
with open('./env.json') as f:
    env = json.load(f)

# connect database
client = MongoClient(host=env["db_host"], port=env["db_port"],
                     username=env["db_user"], password=env["db_pass"],
                     authSource=env["db_name"])
db = client[env["db_name"]]

# Load model
control = VNlp()
control.from_bin('./VNlp/model/wiki.vi.model')
utils = DataUtils()

app = Flask(__name__)
app.json_encoder = MongoJSONEncoder
@app.route('/')
def index():
    return "Hello, World!"

# fetch category
@app.route('/api/category/fetch', methods=['GET'])
def fetch_category():
    category_colection = db["category"]
    categories = list(category_colection.find())
    return jsonify(data=categories)

# fetch news in category
@app.route('/api/news/fetch/<string:category_path>/<int:p_>', methods=['GET'])
def fetch_news(category_path, p_):
    _lim = 20
    _skip = _lim * (p_ - 1)
    post_collection = db["post"]
    if category_path == "trend":
        posts = list(post_collection.find({"trend": True}, {
                     'title': 1, 'id': 1, 'image': 1, 'author': 1, '_id': 0}).skip(_skip).limit(_lim))
        remain_len = len(list(post_collection.find({"trend":True}))) - len(posts) - _skip
    else:
        posts = list(post_collection.find({"category": category_path}, {
                     '_id': False}).skip(_skip).limit(_lim))
        remain_len = len(list(post_collection.find(
            {"category": category_path}))) - len(posts) - _skip
    if len(posts) == 0:
        abort(404)
    return jsonify(posts)


@app.route('/api/news/status/<string:category_path>/<int:p_>', methods=['GET'])
def status_news(category_path, p_):
    _lim = 20
    _skip = _lim * (p_ - 1)
    post_collection = db["post"]
    if category_path == "trend":
        posts = list(post_collection.find({"trend": True}, {
                     'title': 1, 'id': 1, 'image': 1, 'author': 1, '_id': 0}).skip(_skip).limit(_lim))
        remain_len = len(list(post_collection.find({"trend":True}))) - len(posts) - _skip
    else:
        posts = list(post_collection.find({"category": category_path}, {
                     'title': 1, 'id': 1, 'image': 1, 'author': 1, '_id': 0}).skip(_skip).limit(_lim))
        remain_len = len(list(post_collection.find(
            {"category": category_path}))) - len(posts) - _skip
    if len(posts) == 0:
        abort(404)
    _r = remain_len > 0
    return jsonify(_r)

def sumarization(_content):
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
    _out_len = math.floor(len(list(rank.sorted_scores.keys())) * 0.5)
    _out_key = list(rank.sorted_scores.keys())[:_out_len]
    _out_key.sort()
    _out_list = [_sent_list[i] for i in _out_key]
    return _out_list

# get news with id
@app.route('/api/news/get/<int:id_>', methods=['GET'])
def get_news(id_):
    post_collection = db["post"]
    posts = list(post_collection.find({"id": id_}, {'_id': 0}))
    if len(posts) == 0:
        abort(404)
    _content = posts[0]["content"]
    posts[0].update({"summar": _content})
    try:
        pass
        _summar = sumarization(_content)
        posts[0].update({"summar": ".".join(sumarization(_content))})
    except:
        pass
    return jsonify(posts[0])

# error handle
@app.errorhandler(404)
def not_found(error):
    return make_response(jsonify(error="Not found"), 404)
@app.errorhandler(500)
def server_error(error):
    return make_response(jsonify(error="Internal Server Error"), 500)

if __name__ == '__main__':
    app.run(debug=False, host='0.0.0.0', port=1998)
