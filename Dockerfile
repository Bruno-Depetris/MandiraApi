# Etapa base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo de proyecto (está en la raíz)
COPY *.csproj ./

# Restaurar dependencias
RUN dotnet restore

# Copiar todo el código fuente (todo el contenido de la raíz del proyecto)
COPY . .

# Construir el proyecto en Release
RUN dotnet build -c Release -o /app/build

# Publicar el proyecto
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MandiraAPI.dll"]
