# Example Blog API
This project is a reference architecture for a .NET application based around a simple blogging application it features
- Clean resource based APIs with human friendly filtering/sorting options
  - Including fully functional swagger page
- Entity framework configuration including
  - Cascading soft deletes
  - Automatic entity timestamps
- Example docker compose configuration

## Project Setup
- Copy `.env.example` to `.env`
- Start up database
  - `docker compose up -d postgres`
- Apply migrations
  - `dotnet ef database update --project src\ExampleBlog.Infrastructure\ExampleBlog.Infrastructure.csproj --startup-project src\ExampleBlog.Api\ExampleBlog.Api.csproj --context ExampleBlog.Infrastructure.AppDbContext`
- Start dotnet project
