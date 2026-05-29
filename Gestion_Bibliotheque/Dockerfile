FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier le projet
COPY Gestion_Bibliotheque/Gestion_Bibliotheque.csproj Gestion_Bibliotheque/

# Restaurer le projet explicitement
RUN dotnet restore Gestion_Bibliotheque/Gestion_Bibliotheque.csproj

# Copier tout le code
COPY . .

WORKDIR /src/Gestion_Bibliotheque
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Gestion_Bibliotheque.dll"]