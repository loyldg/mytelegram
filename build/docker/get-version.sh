#!/bin/sh
currentDate=`date +%-m%d`
#imageVersion=0.6.$currentDate
version=0.18.$currentDate
imageVersion=$version-alpine
echo version: $version
echo image version: $imageVersion