#!/bin/bash

echo "Waiting for RabbitMQ to be available at rabbitmq:5672..."
until nc -z rabbitmq 5672; do
  sleep 2
done

echo "RabbitMQ is up! Starting service..."

echo "Waiting for PostgreSQL at $POSTGRES_HOST:$POSTGRES_PORT..."

# wait for postgres to be ready
until nc -z postgres 5432; do
  echo "Waiting for PostgreSQL..."
  sleep 2
done

echo "PostgreSQL is up - running migrations..."

# Apply EF Core migrations
dotnet NotificationService.dll --migrate || true

# Start the app
dotnet NotificationService.dll
