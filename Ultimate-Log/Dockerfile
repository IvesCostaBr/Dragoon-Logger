FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ultimate-Log.csproj", "./"]
RUN dotnet restore "Ultimate-Log.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Ultimate-Log.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ultimate-Log.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ultimate-Log.dll"]
