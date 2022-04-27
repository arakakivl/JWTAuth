FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5166

ENV ASPNETCORE_URLS=http://+:5166

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["back/JWTAuth.Api/JWTAuth.Api.csproj", "back/JWTAuth.Api/"]
COPY ["back/JWTAuth.Application/JWTAuth.Application.csproj", "back/JWTAuth.Application/"]
COPY ["back/JWTAuth.Infrastructure/JWTAuth.Infrastructure.csproj", "back/JWTAuth.Infrastructure/"]
COPY ["back/JWTAuth.Core/JWTAuth.Core.csproj", "back/JWTAuth.Core/"]
RUN dotnet restore "back/JWTAuth.Api/JWTAuth.Api.csproj"
COPY . .
WORKDIR "/src/back/JWTAuth.Api"
RUN dotnet build "JWTAuth.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JWTAuth.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JWTAuth.Api.dll"]
