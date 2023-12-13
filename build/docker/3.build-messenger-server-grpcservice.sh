#!/bin/sh
#currentDate=`date +%-m%d`
#imageVersion=0.6.$currentDate-alpine
source ./get-version.sh

echo Building mytelegram/mytelegram-messenger-grpc-service:$imageVersion
docker build -t mytelegram/mytelegram-messenger-grpc-service -f ./Dockerfile-messenger-grpc-service ../../source
docker tag mytelegram/mytelegram-messenger-grpc-service mytelegram/mytelegram-messenger-grpc-service:$imageVersion