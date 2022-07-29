#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /sln
ONBUILD COPY *.sln  NuGet.Config ./
COPY ["src/poc.auth.service/poc.auth.service.csproj", "src/poc.auth.service/"]
RUN dotnet restore "src/poc.auth.service/poc.auth.service.csproj"

COPY . .
RUN dotnet build "src/poc.auth.service/poc.auth.service.csproj" -c Release

FROM build AS publish
WORKDIR "/sln/src/poc.auth.service"
RUN dotnet publish "poc.auth.service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "poc.auth.service.dll"]