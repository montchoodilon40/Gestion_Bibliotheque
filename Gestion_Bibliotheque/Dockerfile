FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .

RUN dotnet restore Gestion_Bibliotheque/Gestion_Bibliotheque.csproj
RUN dotnet publish Gestion_Bibliotheque/Gestion_Bibliotheque.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "Gestion_Bibliotheque.dll"]