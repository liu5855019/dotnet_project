#! /bin/bash

count=1

while [ $count -lt 999999999 ]
do
    echo "count is: $count";

    ping 192.168.31.1 -c 2
    if [ $? == 0 ]
    then
        echo "ping is ok";
    else
        echo "ping error"
    fi

    count=$[ $count+1 ]
    sleep 1;
done
