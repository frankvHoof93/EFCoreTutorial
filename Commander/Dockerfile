FROM mcr.microsoft.com/dotnet/core/sdk:3.1.102-alpine AS build-env
WORKDIR /build

COPY Commander.csproj .
RUN dotnet restore Commander.csproj --no-dependencies

COPY . .
RUN dotnet publish Commander.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app

COPY --from=build-env /out ./

ENV ASPNETCORE_URLS http://*

EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT Development
ENV IN_DOCKER true
ENTRYPOINT dotnet Commander.dll
