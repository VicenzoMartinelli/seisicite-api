FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Services.Seisicite.Api/Services.Seisicite.Api.csproj", "Services.Seisicite.Api/"]
RUN dotnet restore "Services.Seisicite.Api/Services.Seisicite.Api.csproj"
COPY . .
WORKDIR "/src/Services.Seisicite.Api"
RUN dotnet build "Services.Seisicite.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Services.Seisicite.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Services.Seisicite.Api.dll"]
