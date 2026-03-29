# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY SimpleAPI.sln ./
COPY Directory.Build.props ./
COPY src/SimpleAPI/SimpleAPI.csproj src/SimpleAPI/

RUN dotnet restore src/SimpleAPI/SimpleAPI.csproj

COPY src/SimpleAPI/ src/SimpleAPI/

RUN dotnet publish src/SimpleAPI/SimpleAPI.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/publish .

USER $APP_UID

ENTRYPOINT ["dotnet", "SimpleAPI.dll"]
