using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheLair.Database
{
    public abstract class Entity<T> where T : Entity<T>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Modified { get; set; } = DateTime.Now;
        public bool Enabled{ get; set; } = true;

        public IEnumerable<U> GetCollection<U>(Func<T, IEnumerable<U>> resolver) where U : Entity<U>
        {
            return (resolver((T)this).Where(i => i.Enabled));
        }

        public abstract void ModelCreating(EntityTypeBuilder<T> model);
    }
}
