FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LogUi/LogUi.csproj", "LogUi/"]
RUN dotnet restore "LogUi/LogUi.csproj"
COPY . .
WORKDIR "/src/LogUi"
RUN dotnet build "LogUi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogUi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogUi.dll"]
