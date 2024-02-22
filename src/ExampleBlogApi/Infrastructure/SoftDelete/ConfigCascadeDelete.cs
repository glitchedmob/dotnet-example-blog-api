using ExampleBlogApi.Database;
using SoftDeleteServices.Configuration;

namespace ExampleBlogApi.Infrastructure.SoftDelete;

public class ConfigCascadeDelete : CascadeSoftDeleteConfiguration<ISoftDelete>
{
    public ConfigCascadeDelete(AppDbContext context) : base(context)
    {
        GetSoftDeleteValue = entity => entity.DeleteLevel;
        SetSoftDeleteValue = (entity, value) =>
        {
            entity.DeleteLevel = value;
            entity.DeletedAt = value == 0 ? null : DateTime.UtcNow;
        };
    }
}
