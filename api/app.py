from flask import Flask
from flask import jsonify
from flask import abort
from flask import make_response

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
@app.route('/api/news/fetch/<string:category_path>', methods=['GET'])
def fetch_news(category_path):
    post_collection = db["post"]
    posts = list(post_collection.find({"category": category_path}))
    if len(posts) == 0:
        abort(404)
    return jsonify(data=posts)

# get news with id
@app.route('/api/news/get/<string:id_>', methods=['GET'])
def get_news(id_):
    post_collection = db["post"]
    posts = list(post_collection.find({"id": id_}))
    print(post)
    if len(posts) == 0:
        abort(404)
    return jsonify(data=posts)

# error handle
@app.errorhandler(404)
def not_found(error):
    return make_response(jsonify(error="Not found"), 404)


if __name__ == '__main__':
    app.run(debug=False,host='0.0.0.0', port=1998)
