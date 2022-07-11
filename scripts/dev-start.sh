#!/usr/bin/env bash

action=$1

export MONGO_DB_USER=${MONGO_DB_USER:-admin}
export MONGO_DB_PASSWORD=${MONGO_DB_PASSWORD:-admin}
export REDIS_PASSWORD=${REDIS_PASSWORD:-admin}
export POSTGRES_USER=${POSTGRES_USER:-admin}
export POSTGRES_PASSWORD=${POSTGRES_PASSWORD:-admin}
export PG_ADMIN_EMAIL=${PG_ADMIN_EMAIL:-admin@domain.com}
export PG_ADMIN_PASSWORD=${PG_ADMIN_PASSWORD:-admin}
export MSSQL_CONN_STRING=${MSSQL_CONN_STRING:-"Server=tcp:orcuslab.database.windows.net,1433;Initial Catalog=orders-dev;Persist Security Info=False;User ID=orcuslab;Password=AdPbYbyrVQpuwLc9;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"}
export RABBITMQ_USER=${RABBITMQ_USER:-admin}
export RABBITMQ_PASS=${RABBITMQ_PASS:-admin}

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
