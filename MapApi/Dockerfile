FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# COPY YOUR EXACT .csproj filename here
COPY ["MapApi.csproj", "C:\\Users\\leona\\Desktop\\Uni\\Full Stack\\Full_Stack_Map_Assignment\\MapApi\\MapApi\\"]

RUN dotnet restore "C:\Users\leona\Desktop\Uni\Full Stack\Full_Stack_Map_Assignment\MapApi\MapApi\MapApi.csproj"

# Copy ALL source code
COPY . .

WORKDIR "/src"
RUN dotnet build "C:\Users\leona\Desktop\Uni\Full Stack\Full_Stack_Map_Assignment\MapApi\MapApi\MapApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "C:\Users\leona\Desktop\Uni\Full Stack\Full_Stack_Map_Assignment\MapApi\MapApi\MapApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MapApi.dll"]
