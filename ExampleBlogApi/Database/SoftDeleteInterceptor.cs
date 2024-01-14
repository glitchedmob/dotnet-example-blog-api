using System.Data.Common;
using ExampleBlogApi.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ExampleBlogApi.Database;

public class SoftDeleteInterceptor : ISaveChangesInterceptor, IDbCommandInterceptor
{
    public const string ForceDeleteFlag = "ForceDelete";

    public InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        if (eventData.Context is null)
        {
            return result;
        }

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete delete })
            {
                continue;
            };

            var forceDelete = (bool)(entry.CurrentValues[ForceDeleteFlag] ?? false);

            if (forceDelete)
            {
                continue;
            }

            entry.State = EntityState.Modified;
            delete.DeletedAt = DateTime.UtcNow;;
        }
        return result;
    }

    public DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        if (eventData is { Context: AppDbContext appDbContext } && appDbContext.IncludeSoftDeletedEntities)
        {
            appDbContext.IncludeSoftDeletedEntities = false;
        }

        return result;
    }
}
