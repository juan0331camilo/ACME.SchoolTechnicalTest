#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ACME.School.WebApi/ACME.School.WebApi.csproj", "ACME.School.WebApi/"]
COPY ["ACME.School.Application/ACME.School.Application.csproj", "ACME.School.Application/"]
COPY ["ACME.School.Domain/ACME.School.Domain.csproj", "ACME.School.Domain/"]
COPY ["ACME.School.Infrastructure/ACME.School.Infrastructure.csproj", "ACME.School.Infrastructure/"]
RUN dotnet restore "ACME.School.WebApi/ACME.School.WebApi.csproj"
COPY . .
WORKDIR "/src/ACME.School.WebApi"
RUN dotnet build "ACME.School.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ACME.School.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ACME.School.WebApi.dll"]