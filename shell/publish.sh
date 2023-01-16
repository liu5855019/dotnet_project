#! /bin/bash

echo "start publish..."
pwd

echo "start git pull"
git pull


cd ../DM.Log.Service/DM.Log.Service
pwd
rm -rf ./bin/publish
echo "start dotnet publish"
dotnet publish --force "DM.Log.Service.csproj" -c Release -o ./bin/publish
echo "publish ok"
cp -rf ./bin/publish/* /root/dockers/dm_log/publish/
echo "cp ok"


cd /root/dockers
pwd
docker-compose up -d --build dm_log


docker logs -f dm_log


