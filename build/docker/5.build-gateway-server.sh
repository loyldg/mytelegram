#!/bin/sh
#currentDate=`date +%-m%d`
#imageVersion=0.6.$currentDate-alpine
source ./get-version.sh

echo Building mytelegram gateway server $imageVersion
docker build -t mytelegram/mytelegram-gateway-server -f ./Dockerfile-linux-musl-x64-gateway-server ../../source
docker tag mytelegram/mytelegram-gateway-server mytelegram/mytelegram-gateway-server:$imageVersion