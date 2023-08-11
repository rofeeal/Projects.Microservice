#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Projects.Cmd/Projects.Cmd.Api/Projects.Cmd.Api.csproj", "Projects.Cmd/Projects.Cmd.Api/"]
COPY ["CQRS-ES/CQRS.Core/CQRS.Core.csproj", "CQRS-ES/CQRS.Core/"]
COPY ["Projects.Common/Projects.Common/Projects.Common.csproj", "Projects.Common/Projects.Common/"]
COPY ["Projects.Cmd/Projects.Cmd.Domain/Projects.Cmd.Domain.csproj", "Projects.Cmd/Projects.Cmd.Domain/"]
COPY ["Projects.Cmd/Projects.Cmd.Infrastructure/Projects.Cmd.Infrastructure.csproj", "Projects.Cmd/Projects.Cmd.Infrastructure/"]
RUN dotnet restore "Projects.Cmd/Projects.Cmd.Api/Projects.Cmd.Api.csproj"
COPY . .
WORKDIR "/src/Projects.Cmd/Projects.Cmd.Api"
RUN dotnet build "Projects.Cmd.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Projects.Cmd.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Projects.Cmd.Api.dll"]