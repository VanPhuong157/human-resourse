# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy toàn bộ thư mục vào container
COPY . .

# Sử dụng đường dẫn tuyệt đối đã tìm thấy bằng lệnh find
RUN dotnet restore "backup-portal-api/Sep490_G49_Api/Sep490_G49_Api/Sep490_G49_Api.csproj"

# Build dự án
RUN dotnet publish "backup-portal-api/Sep490_G49_Api/Sep490_G49_Api/Sep490_G49_Api.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/publish .

# File dll sẽ nằm trong thư mục publish
ENTRYPOINT ["dotnet", "Sep490_G49_Api.dll"]