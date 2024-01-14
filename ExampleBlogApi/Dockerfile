FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ExampleBlogApi/ExampleBlogApi.csproj", "ExampleBlogApi/"]
RUN dotnet restore "ExampleBlogApi/ExampleBlogApi.csproj"
COPY . .
WORKDIR "/src/ExampleBlogApi"
RUN dotnet build "ExampleBlogApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ExampleBlogApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExampleBlogApi.dll"]
