using ExampleBlog.Core.Entities;
using ExampleBlog.Core.Entities.Behaviors;
using ExampleBlog.Infrastructure.Repositories.Interfaces;
using SoftDeleteServices.Concrete;

namespace ExampleBlog.Infrastructure.Repositories;

internal class UserRepository : BaseCrudRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context, CascadeSoftDelServiceAsync<ISoftDelete> softDeleteService) : base(context, softDeleteService)
    {
    }
}
