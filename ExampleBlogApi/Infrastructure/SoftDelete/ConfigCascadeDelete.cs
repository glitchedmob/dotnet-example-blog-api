using ExampleBlogApi.Database;
using SoftDeleteServices.Configuration;

namespace ExampleBlogApi.Infrastructure.SoftDelete;

public class ConfigCascadeDelete : CascadeSoftDeleteConfiguration<ISoftDelete>
{
    public ConfigCascadeDelete(AppDbContext context) : base(context)
    {
        GetSoftDeleteValue = entity => entity.SoftDeleteLevel;
        SetSoftDeleteValue = (entity, value) => { entity.SoftDeleteLevel = value; };
    }
}
