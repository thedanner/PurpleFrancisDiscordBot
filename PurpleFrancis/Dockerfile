#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR "/src"
COPY ["PurpleFrancis/PurpleFrancis.csproj", "PurpleFrancis/"]
COPY ["PurpleFrancis.Sprays/PurpleFrancis.Sprays.csproj", "PurpleFrancis.Sprays/"]
COPY ["PurpleFrancis.ImageSharpExtensions/PurpleFrancis.ImageSharpExtensions.csproj", "PurpleFrancis.ImageSharpExtensions/"]
COPY ["PurpleFrancis.Tests.Unit/PurpleFrancis.Tests.Unit.csproj", "PurpleFrancis.Tests.Unit/"]
RUN dotnet restore "PurpleFrancis/PurpleFrancis.csproj"
RUN dotnet restore "PurpleFrancis.Tests.Unit/PurpleFrancis.Tests.Unit.csproj"
COPY . .
WORKDIR "/src/PurpleFrancis"
RUN dotnet build "PurpleFrancis.csproj" -c Release -o /app/build

FROM build AS test
WORKDIR "/src/PurpleFrancis.Tests.Unit"
RUN dotnet test -c Release "PurpleFrancis.Tests.Unit.csproj"

FROM build AS publish
WORKDIR "/src/PurpleFrancis"
RUN dotnet publish "PurpleFrancis.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PurpleFrancis.dll"]
