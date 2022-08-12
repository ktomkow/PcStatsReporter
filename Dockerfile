FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
COPY . .
RUN dotnet publish -c Release PcStatsReporter.sln -o out
# image

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
# standard entrypoint not used when host on Heroku
# ENTRYPOINT [ "dotnet", "SamplesServer.dll" ]
# We will use this one with parametrized PORT
# CMD ASPNETCORE_URLS=http://*:$PORT dotnet SamplesServer.dll
EXPOSE 11111
EXPOSE 22222
CMD ["dotnet", "PcStatsReporter.AspNetCore.dll"]