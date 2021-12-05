FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ReportsApi/ReportsApi.csproj", "ReportsApi/"]
RUN dotnet restore "ReportsApi/ReportsApi.csproj"
COPY . .
WORKDIR "/src/ReportsApi"
RUN dotnet build "ReportsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportsApi.dll"]
