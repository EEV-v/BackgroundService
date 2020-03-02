FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src

COPY ["BackgroundService/BackgroundService.csproj", "BackgroundService/"]
COPY ["BackgroundService.BusinessLogic/BackgroundService.BusinessLogic.csproj", "BackgroundService.BusinessLogic/"]
COPY ["BackgroundService.BusinessLogic.Contract/BackgroundService.BusinessLogic.Contract.csproj", "BackgroundService.BusinessLogic.Contract/"]

RUN dotnet restore "BackgroundService/BackgroundService.csproj"
COPY . .
WORKDIR "/src/BackgroundService"
RUN dotnet build "BackgroundService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BackgroundService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackgroundService.dll"]
