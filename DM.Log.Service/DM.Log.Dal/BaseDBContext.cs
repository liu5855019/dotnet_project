namespace DM.Log.Dal
{
    using DM.Log.Common;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.Extensions.Configuration;
    using NLog;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    public class BaseDBContext : DbContext 
    {
        private readonly static Logger Logger = LogManager.GetCurrentClassLogger();

        public string connectionString { get; private set; }
        public DbType DataBaseType { get; private set; }

        /// <summary>
        /// If the DbContextOption's database connection is configured, it will use what is already configured.
        /// <para>Else, it will set appsettings.json's "DefaultConnection" as the database connection string and connect to Oracle database. </para>
        /// </summary>
        public BaseDBContext(DbContextOptions options) : base(options)
        {
            var configuration = ConfigurationBuilderExtensions.ConfigurationRoot();

            this.connectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            this.DataBaseType = Enum.Parse<DbType>(configuration.GetSection("ConnectionStrings:DefaultType").Value);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder != null)
            {
                // check if dependency injection has configured
                if (!optionsBuilder.IsConfigured)
                {
                    switch (DataBaseType)
                    {
                        case DbType.Oracle:
                            optionsBuilder.UseOracle(connectionString);
                            break;
                        case DbType.SqlServer:
                            optionsBuilder.UseSqlServer(connectionString);
                            break;
                        case DbType.MySql:
                            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                            break;
                        default:
                            optionsBuilder.UseOracle(connectionString);
                            break;
                    }
                }
                //optionsBuilder.UseUpperSnakeCaseNamingConvention();
            }
        }




        private void SetTableNameFromTableAttr(Type tp, IMutableEntityType entityType, out string tableName)
        {
            var tbAttr = tp.GetCustomAttribute<TableAttribute>();

            var name = entityType.GetTableName();
            tableName = name;

            entityType.SetTableName(name);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    //skip because CAP8 apply database-view with no special rule
                    if (!string.IsNullOrEmpty(entityType.GetViewName()))
                    {
                        continue;
                    }
                    var tp = Type.GetType(entityType.ClrType.AssemblyQualifiedName);
                    if (tp != null)
                    {

                        SetTableNameFromTableAttr(tp, entityType, out var tableName);
                    }
                    else
                    {
                    }
                }
            }

            base.OnModelCreating(modelBuilder);
        }
        /// <summary>
        /// Synchronously, saves all changes made in the UnitOfWork's DbContext to the database.
        /// </summary>
        public virtual new int SaveChanges()
        {
            //UpdateEntitiesOnAddOrUpdate();
            Logger.Trace("Saving changes to database.");
            return base.SaveChanges();
        }

        //private void UpdateEntitiesOnAddOrUpdate()
        //{
        //    foreach (var entity in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added))
        //    {
        //        var saveEntity = entity.Entity as BaseEntity;
        //        saveEntity.CacheUpdateDT = DateTime.Now;

        //        if (entity.State == EntityState.Added)
        //        {
        //            saveEntity.Version = 0;
        //        }
        //        else if (entity.State == EntityState.Modified)
        //        {
        //            saveEntity.Version += 1;
        //        }
        //    }
        //}
        /// <summary>
        /// Asynchronously, saves all changes made in the UnitOfWork's DbContext to the database.
        /// </summary>
        public virtual new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //UpdateEntitiesOnAddOrUpdate();
            Logger.Trace("Saving changes to database.");
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Transactions allow several database operations to be processed in an atomic manner.
        /// <para>Nested transaction is not allowed in Entity Framework Core. </para>
        /// <para>Usage: unitOfWork.BeginTransaction() </para>
        /// </summary>
        public void BeginTransaction()
        {
            if (this.Database.CurrentTransaction != null)
            {
                Logger.Error("There is a started Transaction detected, please use the other transaction instead of starting a new Transaction.");
                //throw new MultipleTransactionException("There is a started Transaction detected, please use the other transaction instead of starting a new Transaction.");
            }
            this.Database.BeginTransaction();
            Logger.Trace("Transaction has began.");
        }
        /// <summary>
        /// Synchronously, save all the changes done in the transaction & commit the transaction into the database
        /// <para>Nested transaction is not allowed in Entity Framework Core. </para>
        /// <para>Usage: unitOfWork.CommitTransaction() </para>
        /// </summary>
        public void CommitTransaction()
        {
            if (this.Database.CurrentTransaction == null)
            {
                Logger.Error("Transaction does not exists. Please create one with UnitOfWork.BeginTransaction()");
                //throw new MultipleTransactionException("Transaction does not exists. Please create one with UnitOfWork.BeginTransaction()");
            }
            SaveChanges();
            this.Database.CommitTransaction();
            Logger.Trace("Transaction is committed.");
        }
        /// <summary>
        /// Asynchronously, save all the changes done in the transaction & commit the transaction into the database
        /// <para>Nested transaction is not allowed in Entity Framework Core. </para>
        /// <para>Usage: unitOfWork.CommitTransaction() </para>
        /// </summary>
        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (this.Database.CurrentTransaction == null)
            {
                Logger.Error("Transaction does not exists. Please create one with UnitOfWork.BeginTransaction()");
                //throw new MultipleTransactionException("Transaction does not exists. Please create one with UnitOfWork.BeginTransaction()");
            }
            await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            this.Database.CommitTransaction();
            Logger.Trace("Transaction is committed.");
        }
        /// <summary>
        /// Rollback all of the changes done in the Transaction
        /// <para>Nested transaction is not allowed in Entity Framework Core. </para>
        /// <para>Usage: unitOfWork.RollbackTransaction() </para>
        /// </summary>
        public void RollbackTransaction()
        {
            if (this.Database.CurrentTransaction == null)
            {
                Logger.Error("Transaction does not exists. Please create one with UnitOfWork.BeginTransaction()");
                //throw new MultipleTransactionException("Transaction does not exists. Please create one with UnitOfWork.BeginTransaction()");
            }
            this.Database.RollbackTransaction();
            Logger.Trace("Transaction has rolled back.");
        }





    }
}
