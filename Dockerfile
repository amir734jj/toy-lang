FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build-env
ARG BUILD_CONFIGURATION=Debug

# Install OpenJDK-11
RUN apt-get update && \
  apt-get install -y openjdk-11-jre-headless && \
  apt-get clean

WORKDIR /app

COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o out

WORKDIR /app/out

ENV ASPNETCORE_URLS http://*:$PORT
ENTRYPOINT ["dotnet", "Playground.dll"]
