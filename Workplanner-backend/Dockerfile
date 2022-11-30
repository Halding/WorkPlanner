FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WorkPlanner.Api/WorkPlanner.Api.csproj", "WorkPlanner.Api/"]
COPY ["WorkPlanner.DataAccess/WorkPlanner.DataAccess.csproj", "WorkPlanner.DataAccess/"]
COPY ["WorkPlanner.Core/WorkPlanner.Core.csproj", "WorkPlanner.Core/"]
COPY ["WorkPlanner.Domain/WorkPlanner.Domain.csproj", "WorkPlanner.Domain/"]
RUN dotnet restore "WorkPlanner.Api/WorkPlanner.Api.csproj"
COPY . .
WORKDIR "/src/WorkPlanner.Api"
RUN dotnet build "WorkPlanner.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkPlanner.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkPlanner.Api.dll"]
