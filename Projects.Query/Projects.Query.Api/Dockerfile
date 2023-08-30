#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Projects.Query/Projects.Query.Api/Projects.Query.Api.csproj", "Projects.Query/Projects.Query.Api/"]
COPY ["CQRS-ES/CQRS.Core/CQRS.Core.csproj", "CQRS-ES/CQRS.Core/"]
COPY ["Projects.Common/Projects.Common/Projects.Common.csproj", "Projects.Common/Projects.Common/"]
COPY ["Projects.Query/Projects.Query.Domain/Projects.Query.Domain.csproj", "Projects.Query/Projects.Query.Domain/"]
COPY ["Projects.Query/Projects.Query.Infrastructure/Projects.Query.Infrastructure.csproj", "Projects.Query/Projects.Query.Infrastructure/"]
RUN dotnet restore "Projects.Query/Projects.Query.Api/Projects.Query.Api.csproj"
COPY . .
WORKDIR "/src/Projects.Query/Projects.Query.Api"
RUN dotnet build "Projects.Query.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Projects.Query.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Projects.Query.Api.dll"]