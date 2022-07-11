#!/usr/bin/env bash

action=$1

export MONGO_DB_USER=${MONGO_DB_USER:-admin}
export MONGO_DB_PASSWORD=${MONGO_DB_PASSWORD:-admin}
export REDIS_PASSWORD=${REDIS_PASSWORD:-admin}
export POSTGRES_USER=${POSTGRES_USER:-admin}
export POSTGRES_PASSWORD=${POSTGRES_PASSWORD:-admin}
export PG_ADMIN_EMAIL=${PG_ADMIN_EMAIL:-admin@domain.com}
export PG_ADMIN_PASSWORD=${PG_ADMIN_PASSWORD:-admin}
export SQL_SERVER_USER=${SQL_SERVER_USER:-sa}
export SQL_SERVER_PASS=${SQL_SERVER_PASS:-aDm1Np@SSW0rD}

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
