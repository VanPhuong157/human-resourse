# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .

# Sử dụng wildcard để tự tìm file csproj dù thư mục có sâu đến đâu
RUN dotnet restore "**/Sep490_G49_Api.csproj"
RUN dotnet publish "**/Sep490_G49_Api.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
# Ép cổng 8080 ở tầng biến môi trường cho chắc chắn
ENV ASPNETCORE_URLS=http://+:8080 
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Sep490_G49_Api.dll"]