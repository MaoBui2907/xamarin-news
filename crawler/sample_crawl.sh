#!/bin/bash
for i in {0..5..1}
do
    scrapy crawl news -a category=$i
done