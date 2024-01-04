import requests

class APIService:
    def __init__(self):
        self.url = "http://{host}:{port}/api".format(host="localhost", port=1998)
        
    def get_categories(self):
        return requests.get(self.url + "/category").json().get('data', [])
    
    def search_news(self, category = None, search_key = None, page = 1):
        params = {}
        
        if category is not None:
            params['category'] = category
        if search_key is not None:
            params['search'] = search_key
            
        params.update({'page': page})
        return requests.get(self.url + "/news", params=params).json().get('data', [])
    
    def get_news_by_id(self, news_id: str, compress = 0.7):
        return requests.get(self.url + "/news/" + news_id, params={'compress': compress}).json().get('data', {})
    

api = APIService()