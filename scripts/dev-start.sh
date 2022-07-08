#!/usr/bin/env bash

export MONGO_DB_USER=${MONGO_DB_USER:-admin}
export MONGO_DB_PASSWORD=${MONGO_DB_PASSWORD:-admin}

COMPOSE="docker-compose"

$COMPOSE -f docker-compose.infrastructure.yml -f docker-compose.development.yml up
