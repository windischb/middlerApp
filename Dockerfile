FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS builder
WORKDIR /sln

COPY . .

RUN dotnet restore --configfile /sln/Nuget.config
RUN dotnet publish /sln/source/middlerApp.API/middlerApp.API.csproj -o ./publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app

COPY --from=builder ./sln/publish .

EXPOSE 80
EXPOSE 443
EXPOSE 4444

ENTRYPOINT ["dotnet", "middlerApp.API.dll"]
