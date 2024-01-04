
FROM python:3.10

WORKDIR /app

COPY . .

COPY crontab /etc/cron.d/crontab

RUN apt-get update && apt-get -y install cron && chmod 0644 /etc/cron.d/crontab && crontab /etc/cron.d/crontab

RUN pip install scrapy pymongo

CMD cron -f
