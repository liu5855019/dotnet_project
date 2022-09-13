#! /bin/bash

count=0
errorCount=0
maxErrorCount=3
sleepSecond=30
url="192.168.31.1"
logPath="/tmp/check_is_online/"
logFile="check_is_online.log"


[ -d $logPath ] || mkdir $logPath

logFile="$logPath`date +\"%F\"`_$logFile"

[ -f $logFile ] || touch $logFile

writeLog()
{
    echo "`date +\"%F %T\"` : $1" >> $logFile
}


while [ $((count++)) -lt 999999999 ]
do
    writeLog "count is: $count";

    ping $url -c 2 > /dev/null
    if [ $? == 0 ]
    then
        writeLog "ping is ok"
        errorCount=0
    else
        writeLog "ping error: $((++errorCount))"

        if [ $errorCount -gt $maxErrorCount ]
        then
            writeLog "begin reboot"
            echo "123456" | sudo -S reboot
        fi
    fi

    sleep $sleepSecond;
done
