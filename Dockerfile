# Imagen base para ASP.NET Core en tiempo de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Imagen base para construir el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos el archivo del proyecto
COPY MandiraAPI.csproj ./
RUN dotnet restore MandiraAPI.csproj

# Copiamos el resto del código
COPY . ./
WORKDIR /src
RUN dotnet build MandiraAPI.csproj -c Release -o /app/build

# Publicamos el proyecto
FROM build AS publish
RUN dotnet publish MandiraAPI.csproj -c Release -o /app/publish /p:UseAppHost=false

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Comando por defecto al ejecutar el contenedor
ENTRYPOINT ["dotnet", "MandiraAPI.dll"]
