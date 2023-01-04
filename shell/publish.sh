git pull

cd DM.Log.Service/

# dotnet publish \
#     -c Release \
#     -r linux-x64 \
#     --self-contained true\
#     -o /root/dockers/projects/log_service/publish
#     -r osx-x64 \

rm -f /root/dockers/projects/log_service/publish/*

dotnet publish \
    -c Release \
    -o /root/dockers/projects/log_service/publish









#! /bin/bash

cd /root/shell/incident/CAD-AppServer-IncidentService
pwd

# git checkout -b cad_team_a remotes/origin/cad_team_a
git checkout incident_publish
git pull

cd /root/shell/incident/CAD-AppServer-IncidentService/SourceCode/Server/Stee.FGMS.CAD.IncidentSrv.GrpcService/Stee.FGMS.CAD.IncidentSrv.GrpcService
rm -rf ./bin/publish
dotnet publish --force "Stee.FGMS.CAD.IncidentSrv.GrpcService.csproj" -c Release -o ./bin/publish
echo "publish ok"
cp -r ./bin/publish /root/devops/cad_incident/
echo "cp ok"

cd /root/devops
docker-compose up -d --build cad_incident

# 放开下面两行 即可自动更新 cad.pb, 重启 envoy9443
cd /root/shell
bash update_cad_pb.sh

docker logs -f cad_incident


