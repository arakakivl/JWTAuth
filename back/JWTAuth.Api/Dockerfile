FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JWTAuth.Api/JWTAuth.Api.csproj", "JWTAuth.Api/"]
RUN dotnet restore "JWTAuth.Api/JWTAuth.Api.csproj"
COPY . .
WORKDIR "/src/JWTAuth.Api"
RUN dotnet build "JWTAuth.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JWTAuth.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JWTAuth.Api.dll"]
