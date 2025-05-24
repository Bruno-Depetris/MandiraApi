# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar el archivo del proyecto y restaurar dependencias
COPY MandiraAPI.csproj ./
RUN dotnet restore

# Copiar el resto del código y publicar en Release
COPY . ./
RUN dotnet publish -c Release -o out /p:UseAppHost=false

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar los archivos publicados desde la etapa build
COPY --from=build /app/out ./

# Exponer puerto 80 (http)
EXPOSE 80

# Comando de inicio
ENTRYPOINT ["dotnet", "MandiraAPI.dll"]
