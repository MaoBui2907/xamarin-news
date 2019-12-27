import re
import scrapy
from scrapy.exceptions import CloseSpider
from scrapy.spiders import Rule, CrawlSpider
from scrapy.linkextractors import LinkExtractor
import csv
import os

from pymongo import MongoClient

run_config = {
    "start_urls": ['https://vnexpress.net/24h-qua'],
    "allowed_domains": ['vnexpress.net'],
    "category":["doi-song","suc-khoe","giao-duc","thoi-su","khoa-hoc","the-thao"],
    "allowed_regex": ["^https://vnexpress.net/doi-song/",
                      "^https://vnexpress.net/suc-khoe/",
                      "^https://vnexpress.net/giao-duc/",
                      "^https://vnexpress.net/thoi-su/",
                      "^https://vnexpress.net/khoa-hoc/",
                      "^https://vnexpress.net/the-thao/"],
    "denied_extensions": ['jpg', 'png', 'pdf', 'jpeg'],
    "rm_none": True,
    "xpath_config": {
        "title": "//h1[contains(@class, \"title_news_detail\")]/text()",
        "keywords": "/html/head/meta[@name=\"keywords\"]/@content",
        "description": "/html/head/meta[@name=\"description\"]/@content",
        "content": "//article[contains(@class, \"content_detail\")]/p[position()!=last()]//text()",
        "author": "//article[contains(@class, \"content_detail\")]/p[last()]/strong//text()",
        "image": "//article[contains(@class, \"content_detail\")]//img/@src"
    }
}

db_config = {
    "db_host": "40.119.210.85",
    "db_port": 27017,
    "db_user": "news",
    "db_pass": "news",
    "db_name": "news"
}


class FullLinks(CrawlSpider):
    name = 'news'

    def __init__(self, category="0", run_config=run_config, db_config=db_config, *args, **kwargs):
        super(FullLinks, self).__init__(*args, **kwargs)
        self.start_urls = run_config["start_urls"]
        self.allowed_domains = run_config["allowed_domains"]
        self.xpath_config = run_config["xpath_config"]
        self.rm_none = run_config["rm_none"]
        self.col_titles = ["url"] + [k for k in self.xpath_config.keys()]
        self.count = 1
        self.max_count = 100
        self.db_config = db_config
        self.category = run_config["category"][int(category)]
        client = MongoClient(host=db_config["db_host"], port=db_config["db_port"],
                             username=db_config["db_user"], password=db_config["db_pass"],
                             authSource=db_config["db_name"])
        db = client[db_config["db_name"]]
        self.collection = db["post"]
        self.init_len_c = self.collection.count()
        self.collection.update_many({"category": self.category}, {"$set": {"trend": False}})
        FullLinks.rules = [Rule(LinkExtractor(allow=run_config["allowed_regex"][int(category)],
                                              deny_extensions=run_config["denied_extensions"]), callback="parse_item", follow=True)]
        super(FullLinks, self)._compile_rules()

    def parse_item(self, response):
        record = {
            "link": response.url,
            "category": self.category,
            "trend": True
        }
        for k, v in self.xpath_config.items():
            if k == "image":
                record.update({k: response.xpath(v).extract()})
            else:
                record.update({k: "".join(response.xpath(v).extract()).strip()})

        if ("" in record.values() or [] in record.values()) and self.rm_none:
            return

        _p = list(self.collection.find({"title": record["title"]}))
        if len(_p) == 0:
            record.update({"id": self.init_len_c + self.count})
            self.collection.insert_one(record)
            print("insert " + str(self.count))
            self.count = self.count + 1
        if self.count > self.max_count:
            raise CloseSpider("reach result's max count")
