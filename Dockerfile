# Sử dụng SDK để build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy và restore project (đường dẫn vào thư mục SmartHR)
COPY ["SmartHR/SmartHR.csproj", "SmartHR/"]
RUN dotnet restore "SmartHR/SmartHR.csproj"

# Copy toàn bộ code và build
COPY . .
WORKDIR "/src/SmartHR"
RUN dotnet build "SmartHR.csproj" -c Release -o /app/build

# Publish ứng dụng
FROM build AS publish
RUN dotnet publish "SmartHR.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image để chạy app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartHR.dll"]