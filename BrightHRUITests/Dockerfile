FROM mcr.microsoft.com/playwright/dotnet:latest

WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet build --configuration Release

RUN ls -la bin/Release/net8.0/

RUN dotnet tool restore

RUN dotnet tool run playwright install

CMD ["dotnet", "test", "--no-build", "--logger", "trx"]