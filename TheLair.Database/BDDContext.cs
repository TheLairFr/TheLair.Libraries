using Microsoft.EntityFrameworkCore;

namespace TheLair.Database
{
    public abstract class BDDContext : DbContext
    {
        public string? ConnectionString;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityBuilder b = new EntityBuilder(modelBuilder);

            ModelCreating(b);
        }

        public abstract void ModelCreating(EntityBuilder builder);
    }

    public abstract class BDDContext<T> : BDDContext 
        where T : BDDContext<T>
    {
        
    }
}
