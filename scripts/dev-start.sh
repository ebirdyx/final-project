#!/usr/bin/env bash

action=$1

export MONGO_DB_USER=${MONGO_DB_USER:-admin}
export MONGO_DB_PASSWORD=${MONGO_DB_PASSWORD:-admin}
export REDIS_PASSWORD=${REDIS_PASSWORD:-admin}
export POSTGRES_USER=${POSTGRES_USER:-admin}
export POSTGRES_PASSWORD=${POSTGRES_PASSWORD:-admin}

COMPOSE="docker-compose"

if [[ "$action" == "services" ]]; then
  $COMPOSE \
    -f docker-compose.infrastructure.yml \
    -f docker-compose.development.yml \
    -f docker-compose.services.yml \
    up --build
else
  $COMPOSE \
    -f docker-compose.infrastructure.yml \
    -f docker-compose.development.yml \
    up
fi
