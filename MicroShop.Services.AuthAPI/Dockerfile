
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
#ARG BUILD_CONFIGURATION=Release
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM base AS build
WORKDIR /src
COPY ["MicroShop.Services.AuthAPI.csproj", "MicroShop.Services.AuthAPI/"]
RUN dotnet restore "MicroShop.Services.AuthAPI/MicroShop.Services.AuthAPI.csproj"

WORKDIR "/src/MicroShop.Services.AuthAPI"
COPY . .
RUN dotnet build "MicroShop.Services.AuthAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroShop.Services.AuthAPI.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
# /p:UseAppHost=false
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroShop.Services.AuthAPI.dll"]
