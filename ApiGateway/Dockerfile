# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app/ApiGateway

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app/ApiGateway

COPY --from=build /app/ApiGateway/out ./

# ASP.NET listens on port 80 by default
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "ApiGateway.dll"]
