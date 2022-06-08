FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Business.API/Business.API.csproj", "Business.API/"]
RUN dotnet restore "Business.API/Business.API.csproj"
COPY . .
WORKDIR "/src/Business.API"
RUN dotnet build "Business.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Business.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Business.API.dll"]
