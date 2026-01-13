FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ENV ASPNETCORE_URLS=http://+:10000
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /src

# Copy csproj FROM correct subfolder location
COPY ["MapApi/MapApi.csproj", "./"]

# Restore
RUN dotnet restore "./MapApi.csproj"

# Copy entire project folder
COPY ["MapApi/", "./"]

WORKDIR "/src"
RUN dotnet build "MapApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MapApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MapApi.dll"]
