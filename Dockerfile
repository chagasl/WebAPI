# Use a imagem base oficial do .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use a imagem do SDK para construir o aplicativo
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebAPI.csproj", "./"]
RUN dotnet restore "./WebAPI.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet publish "./WebAPI.csproj" -c Release -o /app/publish

# Configurar o estágio final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]