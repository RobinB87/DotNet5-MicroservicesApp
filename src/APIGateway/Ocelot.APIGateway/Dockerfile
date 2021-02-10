#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/APIGateway/Ocelot.APIGateway/Ocelot.APIGateway.csproj", "src/APIGateway/Ocelot.APIGateway/"]
RUN dotnet restore "src/APIGateway/Ocelot.APIGateway/Ocelot.APIGateway.csproj"
COPY . .
WORKDIR "/src/src/APIGateway/Ocelot.APIGateway"
RUN dotnet build "Ocelot.APIGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ocelot.APIGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ocelot.APIGateway.dll"]
