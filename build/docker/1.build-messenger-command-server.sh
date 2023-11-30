#!/bin/sh
#currentDate=`date +%-m%d`
#imageVersion=0.6.$currentDate-alpine
source ./get-version.sh

echo Building mytelegram messenger command server $imageVersion
#docker build -t mytelegram-messenger-server:$imageVersion -f ./Dockerfile-messenger-server .
docker build -t mytelegram/mytelegram-messenger-command-server -f ./Dockerfile-messenger-command-server ../../source --network=host
docker tag mytelegram/mytelegram-messenger-command-server:latest mytelegram/mytelegram-messenger-command-server:$imageVersion