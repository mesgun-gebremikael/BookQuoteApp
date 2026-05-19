FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY BookQuoteApi/BookQuoteApi.csproj ./BookQuoteApi/
RUN dotnet restore ./BookQuoteApi/BookQuoteApi.csproj

# Copy everything else and publish
COPY BookQuoteApi/. ./BookQuoteApi/
WORKDIR /src/BookQuoteApi
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "BookQuoteApi.dll", "--urls", "http://0.0.0.0:${PORT}"]
