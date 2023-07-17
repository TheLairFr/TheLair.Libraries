using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheLair.Database
{
    public class EntityBuilder
    {
        internal ModelBuilder Builder;

        public EntityBuilder(ModelBuilder builder)
        {
            Builder = builder;
        }

        public void Build<T>() where T : Entity<T>, new()
        {
            EntityTypeBuilder<T> b = Builder.Entity<T>();
            T instance = new();
            
            b.HasKey(i => i.Id);
            b.Property(i => i.Created);
            b.Property(i => i.Modified);
            b.Property(i => i.Enabled);
            instance.ModelCreating(b);
        }
    }
}
