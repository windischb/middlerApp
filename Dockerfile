FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS builder
WORKDIR /sln

COPY . .

RUN dotnet build "./build" /nodeReuse:false
RUN dotnet run --project build --target Publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app

COPY --from=builder ./sln/output .

EXPOSE 80
EXPOSE 443
EXPOSE 4444

ENTRYPOINT ["dotnet", "middlerApp.API.dll"]
