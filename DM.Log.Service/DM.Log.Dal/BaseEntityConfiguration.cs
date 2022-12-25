namespace DM.Log.Dal
{
    using DM.BaseEntity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Configures the behavior of the property in BaseEntity
        /// </summary>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            if (builder != null)
            {
                builder.HasKey(b => b.Id);
                builder.Property(b => b.Version).IsConcurrencyToken(true);
            }
        }
    }
}
