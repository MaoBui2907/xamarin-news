import json
from pymongo import MongoClient
import os

db_host = os.environ.get('MONGO_HOST', 'localhost')
db_port = os.environ.get('MONGO_PORT', '27017')
db_user = os.environ.get('MONGO_USER', 'root')
db_pass = os.environ.get('MONGO_PASS', 'root')
db_name = os.environ.get('MONGO_NAME', 'news')

# connect database
client = MongoClient(host=db_host, port=int(db_port),
                     username=db_user, password=db_pass)

post_collection = client.get_database(db_name).get_collection("post")

with open('./sample_10.json', 'w', encoding='utf-8') as f:
    posts = list(post_collection.find({}, { '_id': 0 }).limit(10))
    json.dump(posts, f, ensure_ascii=False)

with open('./all.json', 'w', encoding='utf-8') as f:
    posts = list(post_collection.find({}, { '_id': 0 }))
    json.dump(posts, f, ensure_ascii=False)
    
categories = list(post_collection.distinct('category'))

category_metadata = []
for i, category in enumerate(categories):
    with open(f'./{i}.json', 'w', encoding='utf-8') as f:
        posts = list(post_collection.find({'category': category}, { '_id': 0 }))
        category_metadata.append({"key": i, "category": category, "count": len(posts)})
        json.dump(posts, f, ensure_ascii=False)
    
with open('./category.json', 'w', encoding='utf-8') as f:
    json.dump(category_metadata, f, ensure_ascii=False)
    