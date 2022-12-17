

//namespace Stee.CAP8.DBAccess
//{
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Linq.Expressions;

//    public class OrderBy
//    {
//        public string PropertyName { get; set; }
//        public bool Ascending { get; set; } = true;
//    }

//    public static class IQueryableExtensions
//    {
//        /// <summary>
//        /// Split orderByStr by ',' 
//        /// Check first part of the split for ascending & descending & Set method OrderBy/OrderByDescending
//        /// subsequent part of the split for ascending & descending & set method ThenBy/ThenByDescending 
//        /// Each property name of the splits will be set into the expression lambda 
//        /// </summary>
//        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, IList<OrderBy> orderByList)
//        {
//            if (orderByList == null || orderByList.Count < 1)
//            {
//                return (IOrderedQueryable<T>)source;
//            }
//            IOrderedQueryable<T> result = null;
//            var count = 0;
//            foreach (var orderBy in orderByList)
//            {
//                count++;
//                // LAMBDA: x => x.[PropertyName]
//                var parameter = Expression.Parameter(typeof(T), "x");
//                Expression property = Expression.Property(parameter, orderBy.PropertyName);
//                var lambda = Expression.Lambda(property, parameter);

//                // to construct method name
//                var newOrderBy = (count == 1) ? "OrderBy" : "ThenBy";
//                if (!orderBy.Ascending)
//                {
//                    newOrderBy += "Descending";
//                }
//                // to get Queryable's method: OrderBy/OrderByDescending/ThenBy/ThenByDescending
//                var orderByMethod = typeof(Queryable).GetMethods().First(x => x.Name == newOrderBy && x.GetParameters().Length == 2);
//                var orderByGeneric = orderByMethod.MakeGenericMethod(typeof(T), property.Type);

//                if (result == null)
//                {
//                    // source query
//                    result = (IOrderedQueryable<T>)orderByGeneric.Invoke(null, new object[] { source, lambda });
//                }
//                else
//                {
//                    // subsequent query result from previous iteration
//                    result = (IOrderedQueryable<T>)orderByGeneric.Invoke(null, new object[] { result, lambda });
//                }
//            }
//            return result;
//        }
//    }
//}
