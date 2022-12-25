//namespace Stee.CAP8.DBAccess
//{
//    using Microsoft.EntityFrameworkCore;
//    using Microsoft.EntityFrameworkCore.Migrations;
//    using Stee.CAP8.Entity;
//    using System.Globalization;
//    using System.Linq;

//    public static class Extensions
//    {
//        public static void AddComment(this MigrationBuilder migrationBuilder, DbContext dbContext)
//        {
//            if (dbContext != null)
//            {
//                foreach (var item in dbContext.GetType().GetProperties().Where(x => x.PropertyType.IsGenericType))
//                {
//                    if (item.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
//                    {
//                        var entity = item.PropertyType.GenericTypeArguments[0];
//                        var entityType = dbContext.Model.FindEntityType(entity);
//                        var tableName = entityType.GetTableName();
//                        var columns = entityType.GetProperties().ToList();
//                        foreach (var prop in entity.GetProperties())
//                        {
//                            if (prop.GetCustomAttributes(typeof(Entity.CommentAttribute), false).FirstOrDefault() is Entity.CommentAttribute attr)
//                            {
//                                var columnName = columns.FirstOrDefault(x => x.Name == prop.Name)?.GetColumnName();
//                                if (columnName != null && attr.Comment != null && migrationBuilder != null)
//                                {
//                                    var comment = attr.Comment.Replace("'", "''", false, CultureInfo.InvariantCulture);
//                                    migrationBuilder.Sql($@"COMMENT ON COLUMN ""{tableName}"".""{columnName}"" IS '{comment}'");
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }
//}
