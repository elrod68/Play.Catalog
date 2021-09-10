#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Play.Inventory/Play.Inventory.csproj", "Play.Inventory/"]
RUN dotnet restore "Play.Inventory/Play.Inventory.csproj"
COPY . .
WORKDIR "/src/Play.Inventory"
RUN dotnet build "Play.Inventory.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Play.Inventory.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Play.Inventory.dll"]