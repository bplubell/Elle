FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY Client/*.csproj ./Client/
COPY Server/*.csproj ./Server/
COPY Shared/*.csproj ./Shared/
RUN dotnet restore

# Copy everything else and build
COPY Client/. ./Client/
COPY Server/. ./Server/
COPY Shared/. ./Shared/
WORKDIR /app/Server
RUN dotnet publish --configuration Release --output out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /app/Server/out ./
ENTRYPOINT ["dotnet", "Elle.Server.dll"]