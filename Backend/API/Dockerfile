# Usar la imagen oficial de .NET Runtime como base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Exponer los puertos necesarios
EXPOSE 8080
EXPOSE 8081

# Usar la imagen oficial de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los archivos de proyecto y restaurar las dependencias
COPY ["API/API.csproj", "API/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Model/Model.csproj", "Model/"]
RUN dotnet restore "API/API.csproj"

# Copiar el resto de los archivos y construir la aplicación
COPY . .
WORKDIR "/src/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Crear la imagen final
FROM base AS final
WORKDIR /app

# Copiar los archivos publicados desde la etapa de publicación
COPY --from=publish /app/publish .

# Definir el usuario no root para mayor seguridad
USER app

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "API.dll"]