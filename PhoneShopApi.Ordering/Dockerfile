#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PhoneShopApi.Ordering/PhoneShopApi.Ordering.csproj", "PhoneShopApi.Ordering/"]
RUN dotnet restore "./PhoneShopApi.Ordering/PhoneShopApi.Ordering.csproj"
COPY . .
WORKDIR "/src/PhoneShopApi.Ordering"
RUN dotnet build "./PhoneShopApi.Ordering.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PhoneShopApi.Ordering.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhoneShopApi.Ordering.dll"]