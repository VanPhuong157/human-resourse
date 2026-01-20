FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Sửa đường dẫn COPY cho đúng với thư mục trên GitHub của bạn
COPY ["backup-portal-api/Sep490_G49_Api/Sep490_G49_Api.csproj", "backup-portal-api/Sep490_G49_Api/"]
COPY ["backup-portal-api/BusinessObjects/BusinessObjects.csproj", "backup-portal-api/BusinessObjects/"]
COPY ["backup-portal-api/DataAccess/DataAccess.csproj", "backup-portal-api/DataAccess/"]
COPY ["backup-portal-api/Repository/Repository.csproj", "backup-portal-api/Repository/"]

RUN dotnet restore "backup-portal-api/Sep490_G49_Api/Sep490_G49_Api.csproj"

COPY . .
WORKDIR "/src/backup-portal-api/Sep490_G49_Api"
RUN dotnet build "Sep490_G49_Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sep490_G49_Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sep490_G49_Api.dll"]