#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR "/src"
COPY ["PurpleFrancis/PurpleFrancisDiscordBot.csproj", "PurpleFrancis/"]
COPY ["PurpleFrancisDiscordBot.Sprays/PurpleFrancisDiscordBot.Sprays.csproj", "PurpleFrancisDiscordBot.Sprays/"]
COPY ["PurpleFrancisDiscordBot.ImageSharpExtensions/PurpleFrancisDiscordBot.ImageSharpExtensions.csproj", "PurpleFrancisDiscordBot.ImageSharpExtensions/"]
COPY ["PurpleFrancisDiscordBot.Tests.Unit/PurpleFrancisDiscordBot.Tests.Unit.csproj", "PurpleFrancisDiscordBot.Tests.Unit/"]
RUN dotnet restore "PurpleFrancis/PurpleFrancisDiscordBot.csproj"
RUN dotnet restore "PurpleFrancisDiscordBot.Tests.Unit/PurpleFrancisDiscordBot.Tests.Unit.csproj"
COPY . .
WORKDIR "/src/PurpleFrancis"
RUN dotnet build "PurpleFrancisDiscordBot.csproj" -c Release -o /app/build

FROM build AS test
WORKDIR "/src/PurpleFrancisDiscordBot.Tests.Unit"
RUN dotnet test -c Release "PurpleFrancisDiscordBot.Tests.Unit.csproj"

FROM build AS publish
WORKDIR "/src/PurpleFrancis"
RUN dotnet publish "PurpleFrancisDiscordBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PurpleFrancisDiscordBot.dll"]
