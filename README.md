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
- For now, you'll need to manually add at least one user into the database
  - Connect using your favorite database tool and run the following query
  - `INSERT INTO "AspNetUsers" ("UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed","PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount") VALUES ('testuser', 'TESTUSER', 'test@test.com', 'TEST@TEST>COM', true, false, false, false, 0);`
- Start dotnet project
  - `cd src/ExampleBlog.Api`
  - `dotnet run`

## Project Structure
- `ExampleBlog.Api`
  - ASP.NET Core project containing
    - Controllers
    - Swagger customizations
    - Dtos and ModelBinders
    - Configuration
    - Automapper
  - No other projects depend on this project
- `ExampleBlog.Core`
  - A class library containing
    - Domain objects
    - Entities (which are used as domain objects in some cases)
    - Interfaces for services
  - This project has no dependencies except those required for EF Entities
- `ExampleBlog.Application`
  - A class library containing
    - Implementations for service interfaces defined in `ExampleBlog.Core`
    - Dependency injection registration for Service implementations
  - This project is where most business logic should live
- `ExampleBlog.Infrastructure`
  - A class library containing
    - EF Core configuration
    - Repository interfaces and implementations
      - Repositories act as a wrapper around EF with automatic CRUD functionality
      - They also provide a place to write more complex DB queries
  - This project is where all interactions with the outside world happen. Databases, external APIs etd

## Misc Notes
- Criteria
  - Represents a set of specifications for how to filter or sort a collection
  - This is used heavily in the domain layer to represent the filter and sort options sent over the rest API
- `CreateGetQueryDtoToCriteriaMapping`
  - This is a helpful method that handles the complexity of mapping the `SortOption`s used in the API layer to the `SortCriteria` in the domain layer
