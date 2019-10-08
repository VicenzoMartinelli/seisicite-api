FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet publish Seisicite.Api -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/Seisicite.Api/out .

#ENTRYPOINT ["dotnet", "Seisicite.Api.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Seisicite.Api.dll