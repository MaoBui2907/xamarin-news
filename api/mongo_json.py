from flask import Flask
from flask.json import JSONEncoder

from bson import json_util

class MongoJSONEncoder(JSONEncoder):
    def default(self, obj): 
        return json_util.default(obj)