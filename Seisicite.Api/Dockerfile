FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app

RUN mkdir /output

# Copy project and publish

COPY . /app

WORKDIR /app/Seisicite.Api
RUN dotnet publish --configuration Debug --output /output

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime

ENV ASPNETCORE_URLS http://*:5001

WORKDIR /app

COPY --from=build-env /output .
EXPOSE 5001

ENTRYPOINT ["dotnet", "Seisicite.Api.dll"]