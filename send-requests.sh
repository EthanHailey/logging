#!/bin/bash

while true
do
    if [[ $(($RANDOM % 10)) -le 7 ]]
    then
        echo -n "Sending GET request @ "; date;
        curl http://localhost:5000/test
    else
        echo -n "Sending POST request @ "; date;
        curl --header "Content-Type: application/json" \
            --request POST \
            --data '{"name": "Automated User"}' \
            http://localhost:5000/test

    fi
    sleep 0.5
done