FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy toàn bộ vào /src
COPY . .

# Tìm chính xác file csproj và restore
RUN dotnet restore "backup-portal-api/Sep490_G49_Api/Sep490_G49_Api/Sep490_G49_Api.csproj"

# Publish bằng đường dẫn tương tự
RUN dotnet publish "backup-portal-api/Sep490_G49_Api/Sep490_G49_Api/Sep490_G49_Api.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
# Ép cổng 8080 ở tầng biến môi trường cho chắc chắn
ENV ASPNETCORE_URLS=http://+:8080 
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Sep490_G49_Api.dll"]