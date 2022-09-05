#! /bin/bash

count=0
errorCount=0
maxErrorCount=3
url="192.168.31.1"

while [ $((count++)) -lt 999999999 ]
do
    echo "count is: $count";

    ping $url -c 2
    if [ $? == 0 ]
    then
        echo "ping is ok";
        errorCount=0
    else
        echo "ping error: $((++errorCount))"

        if [ $errorCount -gt $maxErrorCount ]
        then
            echo "begin reboot"
            reboot
        fi
    fi

    sleep 3;
done
