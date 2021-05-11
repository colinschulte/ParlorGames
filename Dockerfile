FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /DockerSource

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY ParlorGames/*.csproj ./ParlorGames/
RUN dotnet restore

# Copy everything else and build website
COPY ParlorGames/. ./ParlorGames/
WORKDIR /DockerSource/ParlorGames
RUN dotnet publish -c release -o /DockerOutput/Website

# Final stage / image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /DockerOutput/Website
COPY --from=build /DockerOutput/Website ./
#ENTRYPOINT ["dotnet", "ParlorGames.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ParlorGames.dll