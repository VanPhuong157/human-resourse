# Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Sep490_G49_Api/Sep490_G49_Api/*.csproj ./Sep490_G49_Api/
RUN dotnet restore ./Sep490_G49_Api/Sep490_G49_Api.csproj

# Copy everything else and build
COPY Sep490_G49_Api/. ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

RUN mkdir -p /app/Uploads

COPY --from=build-env /app/out .

# Install dotnet-ef tool and ensure it's accessible
RUN apt-get update && apt-get install -y curl libunwind8 gettext && \
    curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 6.0 && \
    ~/.dotnet/dotnet tool install --global dotnet-ef --version 6.0.0 && \
    ln -s ~/.dotnet/tools/dotnet-ef /usr/local/bin/dotnet-ef && \
    export PATH="$PATH:/root/.dotnet:/root/.dotnet/tools"

# Expose port
EXPOSE 80

# Set entrypoint
ENTRYPOINT ["dotnet", "Sep490_G49_Api.dll"]
