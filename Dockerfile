FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["backup-portal-api/Sep490_G49_Api/Sep490_G49_Api/Sep490_G49_Api.csproj", "Sep490_G49_Api/"]
COPY ["backup-portal-api/Sep490_G49_Api/BusinessObjects/BusinessObjects.csproj", "BusinessObjects/"]
COPY ["backup-portal-api/Sep490_G49_Api/DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["backup-portal-api/Sep490_G49_Api/Repository/Repository.csproj", "Repository/"]

RUN dotnet restore "Sep490_G49_Api/Sep490_G49_Api.csproj"
COPY . .
WORKDIR "/src/backup-portal-api/Sep490_G49_Api/Sep490_G49_Api"
RUN dotnet publish -c Release -o /app/publish

# --- THÊM ĐOẠN NÀY ĐỂ CHẠY ---
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/publish .
# Lệnh chạy ứng dụng
ENTRYPOINT ["dotnet", "Sep490_G49_Api.dll"]
