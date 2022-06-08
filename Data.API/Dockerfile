FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Data.API/Data.API.csproj", "Data.API/"]
RUN dotnet restore "Data.API/Data.API.csproj"
COPY . .
WORKDIR "/src/Data.API"
RUN dotnet build "Data.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Data.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Data.API.dll"]
