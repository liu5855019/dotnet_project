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

