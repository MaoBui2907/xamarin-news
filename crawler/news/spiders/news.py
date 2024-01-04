import hashlib
import uuid
from scrapy.spiders import Rule, CrawlSpider
from scrapy.linkextractors import LinkExtractor
import os

from pymongo import MongoClient

run_config = {
    "start_urls": ['https://vnexpress.net/tin-tuc-24h'],
    "allowed_domains": ['vnexpress.net'],
    "allowed_regex": "^https://vnexpress.net/",
    "denied_extensions": ['jpg', 'png', 'pdf', 'jpeg'],
    "rm_none": True,
    "xpath_config": {
        "title": "//h1[contains(@class, \"title-detail\")]/text()",
        "category": "//ul[contains(@class, \"breadcrumb\")]/li[1]/a/@title",
        "keywords": "/html/head/meta[@name=\"keywords\"]/@content",
        "publish_time": "/html/head/meta[@name=\"pubdate\"]/@content",
        "last_modified": "/html/head/meta[@name=\"lastmod\"]/@content",
        "description": "/html/head/meta[@name=\"description\"]/@content",
        "content": "//article[contains(@class, \"fck_detail\")]/p[position()!=last()]//text()",
        "author": "//article[contains(@class, \"fck_detail\")]/p[last()]/strong//text()",
        "image": "//article[contains(@class, \"fck_detail\")]//img[1]/@data-src"
    }
}

db_config = {
    "db_host": os.environ.get('MONGO_HOST', 'localhost'),
    "db_port": int(os.environ.get('MONGO_PORT', '27017')),
    "db_user": os.environ.get('MONGO_USER', 'root'),
    "db_pass": os.environ.get('MONGO_PASS', 'root'),
    "db_name": os.environ.get('MONGO_NAME', 'news')
}


class FullLinks(CrawlSpider):
    name = 'news'

    def __init__(self, run_config=run_config, db_config=db_config, *args, **kwargs):
        super(FullLinks, self).__init__(*args, **kwargs)
        self.start_urls = run_config["start_urls"]
        self.allowed_domains = run_config["allowed_domains"]
        self.xpath_config = run_config["xpath_config"]
        self.rm_none = run_config["rm_none"]
        self.col_titles = ["url"] + [k for k in self.xpath_config.keys()]
        self.db_config = db_config
        client = MongoClient(host=db_config["db_host"], port=db_config["db_port"],
                             username=db_config["db_user"], password=db_config["db_pass"])
        db = client.get_database(db_config["db_name"])
        self.collection = db.get_collection("post")
        
        FullLinks.rules = [Rule(LinkExtractor(allow=run_config["allowed_regex"],
                                              deny_extensions=run_config["denied_extensions"]), callback="parse_item", follow=True)]
        super(FullLinks, self)._compile_rules()

    def parse_item(self, response):
        record = {
            "link": response.url,
            "trend": True
        }
        for k, v in self.xpath_config.items():
            if k == "image":
                record.update({k: response.xpath(v).get()})
            else:
                record.update({k: "".join(response.xpath(v).extract()).strip()})

        if ("" in record.values() or [] in record.values() or None in record.values()) and self.rm_none:
            return
        else:
            record.update({"id": str(uuid.UUID(hashlib.md5(record['link'].encode('utf-8')).hexdigest()))})
            self.collection.replace_one({"id": record['id']}, record, upsert=True)
