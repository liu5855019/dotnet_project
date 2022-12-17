//namespace Stee.CAP8.DBAccess
//{
//    using Microsoft.EntityFrameworkCore;
//    using Microsoft.EntityFrameworkCore.Metadata.Builders;
//    using Stee.CAP8.Entity;

//    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
//    {
//        /// <summary>
//        /// Configures the behavior of the property in BaseEntity
//        /// </summary>
//        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
//        {
//            if (builder != null)
//            {
//                builder.HasKey(b => b.Id);
//                builder.Property(b => b.Version).IsConcurrencyToken(true);
//            }
//        }
//    }
//}
