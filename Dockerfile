# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj y restaurar dependencias
COPY DragonBallZ.csproj ./
RUN dotnet restore DragonBallZ.csproj

# Copiar todo el código
COPY . ./

# Publicar en carpeta /app/out
RUN dotnet publish DragonBallZ.csproj -c Release -o /app/out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Render define PORT; lo usamos aquí
ENV ASPNETCORE_URLS=http://+:${PORT}

# Copiar la build publicada
COPY --from=build /app/out ./

# Ejecutar tu app
ENTRYPOINT ["dotnet", "DragonBallZ.dll"]
