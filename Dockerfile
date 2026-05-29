FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# copier tout le repo
COPY . .

# aller dans le dossier du projet
WORKDIR /app/Gestion_Bibliotheque

# restore + publish
RUN dotnet restore
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /out .

ENTRYPOINT ["dotnet", "Gestion_Bibliotheque.dll"]