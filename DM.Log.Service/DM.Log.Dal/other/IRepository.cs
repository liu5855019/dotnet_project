//namespace Stee.CAP8.DBAccess
//{
//    using Stee.CAP8.Entity;
//    using System;
//    using System.Collections.Generic;
//    using System.Linq.Expressions;
//    using System.Threading;
//    using System.Threading.Tasks;

//    public interface IRepository<T> : IDisposable where T : BaseEntity
//    {
//        void Add(T entity);
//        void AddRange(params T[] entities);
//        void AddRange(IEnumerable<T> entities);

//        Task AddAsync(T entity, CancellationToken cancellationToken = default);
//        Task AddRangeAsync(CancellationToken cancellationToken = default, params T[] entities);
//        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

//        void Update(T entity);
//        void UpdateRange(params T[] entities);
//        void UpdateRange(IEnumerable<T> entities);
//        void UpdateProperty(long id, string propertyName, object value);

//        void Delete(T entity, bool softDelete = true);
//        void DeleteByID(long id, bool softDelete = true);

//        T GetSingle(Expression<Func<T, bool>> condition = null);
//        T GetSingle(long id);

//        Task<T> GetSingleAsync(Expression<Func<T, bool>> condition = null, CancellationToken cancellationToken = default);
//        Task<T> GetSingleAsync(long id, CancellationToken cancellationToken = default);

//        T GetSingleOrDefault(Expression<Func<T, bool>> condition = null);
//        T GetSingleOrDefault(long id);

//        Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> condition = null, CancellationToken cancellationToken = default);

//        Task<T> GetSingleOrDefaultAsync(long id, CancellationToken cancellationToken = default);

//        T GetFirstOrDefault(Expression<Func<T, bool>> condition = null);

//        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> condition = null, CancellationToken cancellationToken = default);

//        List<T> GetList(Expression<Func<T, bool>> condition = null, IList<OrderBy> orderBy = null, int pageIndex = 1, int pageSize = 10000, bool activeDatastate = true);
//        Task<List<T>> GetListAsync(Expression<Func<T, bool>> condition = null, IList<OrderBy> orderBy = null, int pageIndex = 1, int pageSize = 10000, bool activeDatastate = true, CancellationToken cancellationToken = default);

//        IList<T> SynchronizePreload(DateTime? lastUpdate, Expression<Func<T, bool>> condition = null, bool activeDataState = true);
//        IList<T> SynchronizeRefresh(IDictionary<long, long> entityVersions, bool activeDataState = true);

//        bool IsExists(long id);
//        bool IsExists(Expression<Func<T, bool>> condition);
//    }
//}
