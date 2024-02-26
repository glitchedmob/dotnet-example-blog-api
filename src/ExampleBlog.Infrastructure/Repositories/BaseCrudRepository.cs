using ExampleBlog.Core.Entities.Behaviors;
using ExampleBlog.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SoftDeleteServices.Concrete;

namespace ExampleBlog.Infrastructure.Repositories;

internal abstract class BaseCrudRepository<TEntityType> : ICrudRepository<TEntityType>
    where TEntityType : class, ISoftDelete
{
    protected readonly AppDbContext Context;
    protected readonly CascadeSoftDelServiceAsync<ISoftDelete> SoftDeleteService;

    public BaseCrudRepository(AppDbContext context, CascadeSoftDelServiceAsync<ISoftDelete> softDeleteService)
    {
        Context = context;
        SoftDeleteService = softDeleteService;
    }

    protected DbSet<TEntityType> DbSet => Context.Set<TEntityType>();


    public IQueryable<TEntityType> NewQuery()
    {
        return DbSet.AsQueryable();
    }

    public void Add(TEntityType entity, CancellationToken ct = default)
    {
        DbSet.Add(entity);
    }

    public async Task<int> SaveChanges(CancellationToken ct = default)
    {
        return await Context.SaveChangesAsync();
    }

    public void ForceDelete(TEntityType entity)
    {
        Context.Remove(entity);
    }

    public void Update(TEntityType entity)
    {
        Context.Update(entity);
    }

    public async Task SoftDelete(TEntityType entity, CancellationToken ct = default)
    {
        await SoftDeleteService.SetCascadeSoftDeleteAsync(entity);
    }
}
