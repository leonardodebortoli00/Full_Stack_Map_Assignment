FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MapShortestPathApi.csproj", "."]
RUN dotnet restore "MapShortestPathApi.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "MapShortestPathApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MapShortestPathApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MapShortestPathApi.dll"]
