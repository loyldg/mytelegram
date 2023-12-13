#!/bin/sh
#currentDate=`date +%-m%d`
#imageVersion=0.6.$currentDate-alpine
source ./get-version.sh

echo Building mytelegram/mytelegram-messenger-query-server:$imageVersion
#docker build -t mytelegram-messenger-server:$imageVersion -f ./Dockerfile-messenger-server .
docker build -t mytelegram/mytelegram-messenger-query-server -f ./Dockerfile-messenger-query-server ../../source --network=host
docker tag mytelegram/mytelegram-messenger-query-server:latest mytelegram/mytelegram-messenger-query-server:$imageVersion