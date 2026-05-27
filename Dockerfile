# === 1. FÁZE: SESTAVENÍ (BUILD) ===
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
 
COPY ["AlgoTrace/AlgoTrace.csproj", "AlgoTrace/"]
RUN dotnet restore "AlgoTrace/AlgoTrace.csproj"
 
COPY . .
RUN dotnet publish "AlgoTrace/AlgoTrace.csproj" -c Release -o /app/publish
 
# === 2. FÁZE: BĚHOVÉ PROSTŘEDÍ (SERVE) ===
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
 
RUN rm -rf ./*
COPY --from=build /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/conf.d/default.conf
 
EXPOSE 8080