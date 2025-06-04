#!/bin/bash

echo "Waiting for PostgreSQL at $POSTGRES_HOST:$POSTGRES_PORT..."

# wait for postgres to be ready
until nc -z postgres 5432; do
  echo "Waiting for PostgreSQL..."
  sleep 2
done

echo "PostgreSQL is up - running migrations..."

# Apply EF Core migrations
dotnet TransactionService.dll --migrate || true

# Start the app
dotnet TransactionService.dll
