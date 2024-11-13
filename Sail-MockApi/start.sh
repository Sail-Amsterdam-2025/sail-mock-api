#!/bin/bash

# -d: detached mode
# -p: port mapping
# --name: container name
# sail-mockapi:latest: image name
docker run -d -p 8080:8080 -p 8081:8081 --name sail-mockapi-container sail-mockapi:latest

# docker-compose up --build