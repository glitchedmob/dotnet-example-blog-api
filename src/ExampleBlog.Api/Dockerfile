FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ExampleBlog.Api/ExampleBlog.Api.csproj", "ExampleBlog.Api/"]
COPY ["src/ExampleBlog.Api/.config/dotnet-tools.json", "ExampleBlog.Api/.config/"]
COPY ["src/ExampleBlog.Application/ExampleBlog.Application.csproj", "ExampleBlog.Application/"]
COPY ["src/ExampleBlog.Core/ExampleBlog.Core.csproj", "ExampleBlog.Core/"]
COPY ["src/ExampleBlog.Infrastructure/ExampleBlog.Infrastructure.csproj", "ExampleBlog.Infrastructure/"]
RUN dotnet restore "ExampleBlog.Api/ExampleBlog.Api.csproj"
WORKDIR "/src/ExampleBlog.Api"
RUN dotnet tool restore
WORKDIR /src
COPY src/ .
WORKDIR "/src/ExampleBlog.Api"
RUN dotnet build "ExampleBlog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build/

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ExampleBlog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish/ /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExampleBlog.Api.dll"]
