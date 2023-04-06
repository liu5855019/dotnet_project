#! /bin/bash

echo "start publish..."
pwd

echo "start git pull"
# git pull
echo "git pull done"

rm -rf /root/dockers/dm_log/DM.Log.Service/*
echo "rm ok"
cp -rf ../DM.Log.Service/* /root/dockers/dm_log/DM.Log.Service/
echo "cp ok"



cd /root/dockers
pwd
docker-compose up -d --build dm_log


# docker logs -f dm_log


