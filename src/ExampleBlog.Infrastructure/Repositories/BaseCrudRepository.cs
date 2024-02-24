using ExampleBlog.Core.Domain.Common;
using ExampleBlog.Core.Entities.Behaviors;
using ExampleBlog.Infrastructure.Extensions;
using ExampleBlog.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SoftDeleteServices.Concrete;

namespace ExampleBlog.Infrastructure.Repositories;

internal abstract class BaseCrudRepository<TEntityType> : ICrudRepository<TEntityType>
    where TEntityType : class, ISoftDelete
{
    protected readonly AppDbContext Context;
    private readonly CascadeSoftDelServiceAsync<ISoftDelete> _softDeleteService;

    public BaseCrudRepository(AppDbContext context, CascadeSoftDelServiceAsync<ISoftDelete> softDeleteService)
    {
        Context = context;
        _softDeleteService = softDeleteService;
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

    public void HardDelete(TEntityType entity)
    {
        Context.Remove(entity);
    }

    public void Update(TEntityType entity)
    {
        Context.Update(entity);
    }

    public async Task SoftDelete(TEntityType entity, CancellationToken ct = default)
    {
        await _softDeleteService.SetCascadeSoftDeleteAsync(entity);
    }

    protected IQueryable<TTimeStampedEntityType>
        QueryFromDefaultCriteria<TTimeStampedEntityType>(DefaultQueryCriteria criteria)
        where TTimeStampedEntityType : class, TEntityType, ITimeStamped
    {
        var query = Context.Set<TTimeStampedEntityType>().AsQueryable();

        query = query.ApplyFieldCriteria(e => e.CreatedAt, criteria.CreatedAt);
        query = query.ApplyFieldCriteria(e => e.UpdatedAt, criteria.UpdatedAt);

        if (criteria.SearchText is not null)
        {
            query = (IQueryable<TTimeStampedEntityType>)ApplySearchCriteria(query, criteria.SearchText);
        }

        query = query.Skip(criteria.Offset).Take(criteria.Limit);

        return query;
    }

    protected abstract IQueryable<TEntityType> ApplySearchCriteria(IQueryable<TEntityType> query, string searchText);
}
