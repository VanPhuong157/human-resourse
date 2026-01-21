FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Bước này là 'vàng': Chỉ copy file project để Docker lưu cache thư viện
COPY ["backup-portal-api/Sep490_G49_Api/Sep490_G49_Api/Sep490_G49_Api.csproj", "Sep490_G49_Api/"]
COPY ["backup-portal-api/Sep490_G49_Api/BusinessObjects/BusinessObjects.csproj", "BusinessObjects/"]
COPY ["backup-portal-api/Sep490_G49_Api/DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["backup-portal-api/Sep490_G49_Api/Repository/Repository.csproj", "Repository/"]

RUN dotnet restore "Sep490_G49_Api/Sep490_G49_Api.csproj"

# Sau khi restore xong mới copy code (Lần sau sửa code bước trên sẽ mất 0 giây)
COPY . .
WORKDIR "/src/backup-portal-api/Sep490_G49_Api/Sep490_G49_Api"
RUN dotnet publish -c Release -o /app/publish
