namespace DM.Log.Dal
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Linq;
    using System.Reflection;

    public class LogDBContext : BaseDBContext
    {
        public LogDBContext(DbContextOptions<LogDBContext> options) : base(options, DbType.MySql)
        {
            Console.WriteLine(options);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //获取派生自BaseEntityConfiguration类的配置信息
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => !string.IsNullOrEmpty(x.Namespace)
                && x.BaseType != null
                && x.BaseType.IsGenericType
                && x.BaseType.GetGenericTypeDefinition() == typeof(BaseEntityConfiguration<>));
            foreach (var type in types)
            {
                //动态创建实例对象，添加到modelBuilder里
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.UseLoggerFactory(
                LoggerFactory.Create(
                    delegate (ILoggingBuilder builder)
                    {
                        builder.AddFilter(
                            (string category, Microsoft.Extensions.Logging.LogLevel level) =>
                                category == LoggerCategory<DbLoggerCategory.Database.Command>.Name
                                && (level == Microsoft.Extensions.Logging.LogLevel.Debug
                                    || level == Microsoft.Extensions.Logging.LogLevel.Warning
                                    || level == Microsoft.Extensions.Logging.LogLevel.Error
                                    || level == Microsoft.Extensions.Logging.LogLevel.Critical)
                            );
                    }
                )
           );
        }

        #region DbSet

        public DbSet<LogInterface> LogInterface { get; set; }

        #endregion

    }
}
