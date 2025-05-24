# Usa la imagen oficial de .NET 8 SDK
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore "./MandiraAPI.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "MandiraAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MandiraAPI.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MandiraAPI.dll"]
