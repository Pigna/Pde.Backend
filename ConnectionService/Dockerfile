FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ConnectionService/ConnectionService.csproj", "ConnectionService/"]
RUN dotnet restore "ConnectionService/ConnectionService.csproj"
COPY . .
WORKDIR "/src/ConnectionService"
RUN dotnet build "ConnectionService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConnectionService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConnectionService.dll"]
