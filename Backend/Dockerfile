# Etapa base con el runtime de .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos del proyecto (.csproj) y restaurar dependencias
COPY ./API/API.csproj ./API/
COPY ./Data/Data.csproj ./Data/
COPY ./Model/Model.csproj ./Model/

RUN dotnet restore API/API.csproj

# Copiar el resto del código y compilar la aplicación
COPY . .
WORKDIR /src/API
RUN dotnet build -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish --no-restore

# Imagen final con el runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "API.dll"]
