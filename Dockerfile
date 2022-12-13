FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
RUN curl https://api.nuget.org/v3/index.json
WORKDIR /app
COPY . .
RUN dotnet restore

#RUN dotnet build
RUN dotnet publish -c Release src/PcStatsReporter.AspNetCore/PcStatsReporter.AspNetCore.csproj -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 11111
EXPOSE 22222
ENTRYPOINT ["dotnet", "PcStatsReporter.AspNetCore.dll"]
