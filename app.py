#!flask/bin/python
from flask import Flask
from flask import jsonify
from flask import abort

app = Flask(__name__)


@app.route('/')
def index():
    return "Hello, World!"

# fetch news list


list_news = [{'id': '1', 'title': "Bệnh nhân sốt rét bị tiêm nhầm thuốc chuột", 'image': 'hello.png'}, {
    'id': '2', 'title': "Cá rô cắn chết chuột nhiễm ký sinh", 'image': 'hello2.png'}]


@app.route('/api/news/fetch', methods=['GET'])
def fetch_news():
    return jsonify({'data': list_news})


@app.route('/api/news/<string:id>', methods=['GET'])
def get_news(id):
    news = [news for news in list_news if news['id'] == id]
    if len(news) == 0:
        abort(404)
    return jsonify({'data': news})


if __name__ == '__main__':
    app.run(debug=True)
