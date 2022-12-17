
namespace DM.Log.Dal
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.Extensions.Configuration;
    using NLog;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using DM.Log.Common;
    using Oracle.ManagedDataAccess.Client;

    public class BaseDBContext : DbContext 
    {

        private string connectionString;
        public DbType DataBaseType { get; private set; }

        private readonly static Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly IConfigurationRoot configuration = ConfigurationRoot();


        static BaseDBContext()
        {
            var encryption = configuration.GetConnectionString("Encryption");
            var encryptionTypes = configuration.GetConnectionString("EncryptionTypes");

            if (!string.IsNullOrEmpty(encryption))
            {
                OracleConfiguration.SqlNetEncryptionClient = encryption;
                OracleConfiguration.SqlNetEncryptionTypesClient = !string.IsNullOrEmpty(encryptionTypes) ? encryptionTypes : "(AES256)";
            }
        }

        public BaseDBContext(DbType dbType = DbType.Oracle) : this(null, dbType) { }


        /// <summary>
        /// If the DbContextOption's database connection is configured, it will use what is already configured.
        /// <para>Else, it will set appsettings.json's "DefaultConnection" as the database connection string and connect to Oracle database. </para>
        /// </summary>
        public BaseDBContext(DbContextOptions options) : base(options)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                SetDefaultConnectionString(fromDI: true);
            }
        }

        public BaseDBContext(string conStr, DbType dbType = DbType.Oracle)
        {
            InitializeDBContext(conStr, dbType);
        }

        private void InitializeDBContext(string conStr, DbType dbType)
        {
            if (string.IsNullOrEmpty(conStr))
            {
                SetDefaultConnectionString(dbType);
            }
            else
            {
                connectionString = conStr;
                DataBaseType = dbType;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder != null)
            {
                var logFilterMode = configuration.GetSection("DBAccessLib:LogFilter").Value;
                //if (logFilterMode == null)
                //{
                //    // for backward compatiability with existing usage
                //    // (default) includes Entity Framework Core /WARN/ERROR/CRITICAL logs
                //    optionsBuilder.UseLoggerFactory(NlogLoggerFactoryFiltered);
                //}
                //else
                //{
                //    switch (logFilterMode.ToUpperInvariant())
                //    {
                //        case "DEBUG":
                //            // includes Entity Framework Core DEBUG/WARN/ERROR/CRITICAL logs
                //            optionsBuilder.UseLoggerFactory(NlogLoggerFactoryDebug);
                //            break;
                //        case "NONE":
                //            // includes Entity Framework Core TRACE/INFO/DEBUG/WARN/ERROR/CRITICAL logs
                //            optionsBuilder.UseLoggerFactory(NlogLoggerFactoryAll);
                //            break;
                //        default:
                //            // (default) includes Entity Framework Core /WARN/ERROR/CRITICAL logs
                //            optionsBuilder.UseLoggerFactory(NlogLoggerFactoryFiltered);
                //            break;
                //    }
                //}
                // check if dependency injection has configured
                if (!optionsBuilder.IsConfigured)
                {
                    switch (DataBaseType)
                    {
                        case DbType.Oracle:
                            optionsBuilder.UseOracle(connectionString);
                            break;
                        //case DbType.SqlServer:
                        //    optionsBuilder.UseSqlServer(connectionString);
                        //    break;
                        //case DbType.MariaDB:
                        //    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                        //    break;
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

        /// <summary>
        /// Sets default connection string from appsettings.json.
        /// <para><paramref name="fromDI"/> indicates if the context is injected from dependency injection, sets to UseOracle(true/false) or DbType from appsettings.json when it is not configured.</para>
        /// </summary>
        private void SetDefaultConnectionString(DbType dbType = DbType.Oracle, bool fromDI = false)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            if (fromDI)
            {
                if (!string.IsNullOrEmpty(configuration["UseOracle"]))
                {
                    if (!bool.TryParse(configuration["UseOracle"], out var useoracle))
                    {
                        throw new ArgumentOutOfRangeException($"Configured UseOracle ({configuration["UseOracle"]})  incorrect, possible values are true/false");
                    }
                    DataBaseType = useoracle ? DbType.Oracle : DbType.SqlServer;
                }
                else if (!string.IsNullOrEmpty(configuration["DbType"]))
                {
                    if (!Enum.TryParse<DbType>(configuration["DbType"], true, out var db))
                    {
                        var names = string.Join(',', Enum.GetNames(typeof(DbType)));
                        throw new ArgumentOutOfRangeException($"Configured DbType ({configuration["DbType"]}) incorrect, possible values are {names}");
                    }
                    DataBaseType = db;
                }
                else
                {
                    DataBaseType = DbType.Oracle;
                }
            }
            else
            {
                DataBaseType = dbType;
            }
        }

        private static IConfigurationRoot ConfigurationRoot()
        {
            return new ConfigurationBuilder().AddAppsettingsJsonFileAndEnvironmentVariables().Build();
        }

    }
}
