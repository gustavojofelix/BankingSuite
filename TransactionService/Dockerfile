# STAGE 1 - build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy everything
COPY . .

# Restore and publish AccountService only
WORKDIR /src/TransactionService
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# STAGE 2 - runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published output only
COPY --from=build /app/publish .

# Copy entrypoint script from source (relative to context)
COPY TransactionService/entrypoint.sh .
RUN chmod +x entrypoint.sh

# Install netcat for DB wait script
RUN apt-get update && apt-get install -y netcat-openbsd

# Expose port and define entrypoint
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["./entrypoint.sh"]

