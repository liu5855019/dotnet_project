//namespace Stee.CAP8.DBAccess
//{
//    using LinqKit;
//    using Microsoft.EntityFrameworkCore;
//    using Stee.CAP8.Entity;
//    using System;
//    using System.Collections.Generic;
//    using System.Globalization;
//    using System.Linq;
//    using System.Linq.Expressions;
//    using System.Threading;
//    using System.Threading.Tasks;

//    public class Repository<T> : BaseRepository, IRepository<T> where T : BaseEntity
//    {
//        protected DbContext DbContext { get; set; }
//        protected DbSet<T> DbSet { get; set; }
//        private readonly string entityType = typeof(T).Name.ToString(CultureInfo.InvariantCulture);
//        /// <summary>
//        /// The constructor uses the unitofwork's context to create DbSet in the Repository
//        /// </summary>
//        public Repository(DbContext dbContext)
//        {
//            DbContext = dbContext ?? throw new NoDbContextException("No DbContext is found in Repository");
//            DbSet = DbContext.Set<T>();
//        }
//        /// <summary>
//        /// Adds an entity into the database.
//        /// <para>This method can be overridden by any class that inherits it.</para>
//        /// </summary>
//        virtual public void Add(T entity)
//        {
//            Logger.Trace($"Add Parameter: entity ({entity})");
//            DbSet.Add(entity);
//            if (entity != null)
//            {
//                Logger.Trace(entityType + " entity (" + entity.Id + ") is added using Add(T).");
//            }
//        }
//        /// <summary>
//        /// Adds an entity into the database.
//        /// <para>Use SaveChangesAsync() to add an entity asynchronously</para>
//        /// <para>This method is async only to allow special value generators, such as the one used by 'Microsoft.EntityFrameworkCore.Metadata.SqlServerValueGenerationStrategy.SequenceHiLo', to access the database asynchronously. For all other cases the non async method should be used.</para>
//        /// <para>This method can be overridden by any class that inherits it.</para>
//        /// </summary>
//        virtual public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
//        {
//            Logger.Trace($"AddAsync Parameter: entity ({entity})");
//            await DbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
//            if (entity != null)
//            {
//                Logger.Trace(entityType + " entity (" + entity.Id + ") is added using AddAsync(T).");
//            }
//        }
//        /// <summary>
//        /// Adds a variable number of entities to the database without the need to create a collection for them
//        /// <para>This method can be overridden by any class that inherits it.</para>
//        /// </summary>
//        virtual public void AddRange(params T[] entities)
//        {
//            Logger.Trace($"AddRange(T[]) Parameter: entities ({entities})");
//            DbSet.AddRange(entities);
//            if (entities != null)
//            {
//                Logger.Trace("Number of parameter " + entityType + " entities are added using AddRange(T[]).");
//            }
//        }
//        /// <summary>
//        /// Adds the elements of the specified collection to the end of the List<T>.
//        /// <para>This method can be overridden by any class that inherits it.</para>
//        /// </summary>
//        virtual public void AddRange(IEnumerable<T> entities)
//        {
//            Logger.Trace($"AddRange(IEnumerable<T>) Parameter: entities ({entities})");
//            DbSet.AddRange(entities);
//            if (entities != null)
//            {
//                Logger.Trace("List of " + entityType + " entities are added using AddRange(IEnumerable<T>).");
//            }
//        }

//        /// <summary>
//        /// Adds a variable number of entities to the database without the need to create a collection for them.
//        /// <para>Use SaveChangesAsync() to add the variable number of entities asynchronously</para>
//        /// <para>This method is async only to allow special value generators, such as the one used by 'Microsoft.EntityFrameworkCore.Metadata.SqlServerValueGenerationStrategy.SequenceHiLo', to access the database asynchronously. For all other cases the non async method should be used.</para>
//        /// <para>This method can be overridden by any class that inherits it.</para>
//        /// </summary>
//        virtual public Task AddRangeAsync(CancellationToken cancellationToken = default, params T[] entities)
//        {
//            Logger.Trace($"AddRangeAsync(T[]) Parameter: entities ({entities})");
//            var addRangeTask = DbSet.AddRangeAsync(entities, cancellationToken);
//            if (entities != null)
//            {
//                Logger.Trace("Number of parameter " + entityType + " entities are added using AddRangeAsync(T[]).");
//            }
//            return addRangeTask;
//        }
//        /// <summary>
//        /// Adds the elements of the specified collection to the end of the List<T>.
//        /// <para>Use SaveChangesAsync() to add the collection of entities asynchronously</para>
//        /// <para>This method is async only to allow special value generators, such as the one used by 'Microsoft.EntityFrameworkCore.Metadata.SqlServerValueGenerationStrategy.SequenceHiLo', to access the database asynchronously. For all other cases the non async method should be used.</para>
//        /// <para>This method can be overridden by any class that inherits it.</para>
//        /// </summary>
//        virtual public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
//        {
//            Logger.Trace($"AddRangeAsync(IEnumerable<T>) Parameter: entities ({entities})");
//            var addRangeTask = DbSet.AddRangeAsync(entities, cancellationToken);
//            if (entities != null)
//            {
//                Logger.Trace("List of " + entityType + " entities are added using AddRangeAsync(IEnumerable<T>).");
//            }
//            return addRangeTask;
//        }

//        /// <summary>
//        /// This method can be overridden by any class that inherits it
//        /// update an entity in the database
//        /// Use SaveChangesAsync to update asynchronously
//        /// </summary>
//        virtual public void Update(T entity)
//        {
//            Logger.Trace($"Update Parameter: entity ({entity})");
//            DbSet.Update(entity);
//            if (entity != null && entity.DataState != DataState.Deleted)
//            {
//                Logger.Trace(entityType + " entity (" + entity.Id + ") is updated using Update(T).");
//            }
//        }
//        /// <summary>
//        /// This method can be overridden by any class that inherits it
//        /// update a variable number of entities to the database without the need to create a collection for them
//        /// Use SaveChangesAsync to update asynchronously
//        /// </summary>
//        virtual public void UpdateRange(params T[] entities)
//        {
//            Logger.Trace($"UpdateRange(T[]) Parameter: entities ({entities})");
//            DbSet.UpdateRange(entities);
//            if (entities != null)
//            {
//                Logger.Trace("Number of parameter " + entityType + " entities are updated using UpdateRange(T[]).");
//            }
//        }
//        /// <summary>
//        /// This method can be overridden by any class that inherits it
//        /// update the elements of the specified collection to the end of the List<T>.
//        /// Use SaveChangesAsync to update asynchronously
//        /// </summary>
//        virtual public void UpdateRange(IEnumerable<T> entities)
//        {
//            Logger.Trace($"UpdateRange(IEnumerable<T>) Parameter: entities ({entities})");
//            DbSet.UpdateRange(entities);
//            if (entities != null)
//            {
//                Logger.Trace("List of " + entityType + " entities are updated using UpdateRange(IEnumerable<T>).");
//            }
//        }
//        /// <summary>
//        /// This method can be overridden by any class that inherits it
//        /// Synchronously, retrieve a record by its with parameter id
//        /// Set Property name with string propertyName
//        /// Set Property value with object value
//        /// </summary>
//        virtual public void UpdateProperty(long id, string propertyName, object value)
//        {
//            Logger.Trace($"UpdateProperty Parameter: id ({id}, propertyName ({propertyName}, value({value})");
//            var item = GetSingle(id);
//            // item will never be null, because EF Core will throw exception
//            item.GetType().GetProperty(propertyName).SetValue(item, value);
//            Logger.Trace(entityType + " entity (" + item.Id + ") is updated by property name using UpdateProperty.");
//        }
//        /// <summary>
//        /// This method can be overridden by any class that inherits it
//        /// <para>Synchronously, delete an entity from the database</para>
//        /// <para><paramref name="softDelete"/> = TRUE (default), sets record's Datastate to deleted but remain in database</para>
//        /// <para><paramref name="softDelete"/> = FALSE, deletes the record from the database</para>
//        /// </summary>
//        virtual public void Delete(T entity, bool softDelete = true)
//        {
//            Logger.Trace($"Delete Parameter: entity ({entity}, softDelete ({softDelete})");
//            if (softDelete)
//            {
//                if (entity != null)
//                {
//                    entity.DataState = DataState.Deleted;
//                }
//                Update(entity);
//                if (entity != null)
//                {
//                    Logger.Trace(entityType + " entity (" + entity.Id + ")'s datastate has been set to deleted by using Delete(T).");
//                }
//            }
//            else
//            {
//                DbSet.Remove(entity);
//                if (entity != null)
//                {
//                    Logger.Trace(entityType + " entity (" + entity.Id + ")'s is deleted from the database by using Delete(T).");
//                }
//            }

//        }
//        /// <summary>
//        /// This method can be overridden by any class that inherits it
//        /// <para>Synchronously, delete an entity from the database by finding its ID</para>
//        /// <para><paramref name="softDelete"/> = TRUE (default), sets record's Datastate to deleted but remain in database</para>
//        /// <para><paramref name="softDelete"/> = FALSE, deletes the record from the database</para>
//        /// </summary>
//        virtual public void DeleteByID(long id, bool softDelete = true)
//        {
//            Logger.Trace($"DeleteByID Parameter: id ({id}, softDelete ({softDelete})");
//            // item will never be null, because EF Core will throw exception
//            var item = GetSingle(id);
//            if (softDelete)
//            {
//                item.DataState = DataState.Deleted;
//                Update(item);
//                Logger.Trace(entityType + " entity (" + item.Id + ")'s datastate has been set to deleted by using DeleteByID.");
//            }
//            else
//            {
//                DbSet.Remove(item);
//                Logger.Trace(entityType + " entity (" + item.Id + ")'s is deleted from the database by using DeleteByID.");
//            }
//        }
//        /// <summary>
//        /// Synchronously, query a record from the database with a condition LinQ expression.
//        /// <para>If there are more than one record, EF core will throw exception. </para>
//        /// <para>Example: GetSingle(x => x.ID.ToString(CultureInfo.InvariantCulture) == "ABC");</para>
//        /// </summary>
//        public T GetSingle(Expression<Func<T, bool>> condition = null)
//        {
//            Logger.Trace($"GetSingle Parameter: condition ({condition})");
//            var model = default(T);

//            IQueryable<T> query = DbSet;

//            if (condition != null)
//            {
//                model = query.Single(condition);
//            }
//            else
//            {
//                model = query.Single();
//            }

//            // result will never be null, because EF Core will throw exception
//            Logger.Trace(entityType + " entity (" + model.Id + $") is read by GetSingle with condition ({condition}).");

//            return model;
//        }
//        /// <summary>
//        /// Synchronously, query a record from the database by using the long.
//        /// <para>If there are more than one record, EF core will throw exception. </para>
//        /// </summary>
//        public T GetSingle(long id)
//        {
//            Logger.Trace($"GetSingle Parameter: id ({id})");
//            var result = DbSet.Single(x => x.Id == id);
//            // result will never be null, because EF Core will throw exception
//            Logger.Trace(entityType + " entity (" + result.Id.ToString(CultureInfo.InvariantCulture) + ") is read by GetSingle(id).");
//            return result;
//        }
//        /// <summary>
//        /// Asynchronously, query a record from the database with a condition LinQ expression
//        /// <para>If there are more than one record, EF core will throw exception. </para>
//        /// <para>Example: GetSingleAsync(x => x.ID.ToString(CultureInfo.InvariantCulture) == "ABC");</para>
//        /// </summary>
//        public Task<T> GetSingleAsync(
//            Expression<Func<T, bool>> condition = null,
//            CancellationToken cancellationToken = default)
//        {
//            Logger.Trace($"Start GetSingleAsync Parameter: condition ({condition})");
//            IQueryable<T> query = DbSet;

//            Task<T> task = null;

//            if (condition != null)
//            {
//                task = query.SingleAsync(condition, cancellationToken);
//            }
//            else
//            {
//                task = query.SingleAsync(cancellationToken);
//            }

//            Logger.Trace($"End GetSingleAsync Parameter: condition ({condition})");

//            return task;
//        }
//        /// <summary>
//        /// Asynchronously, query a record from the database by using the long.
//        /// <para>If there are more than one record, EF core will throw exception. </para>
//        /// </summary>
//        public Task<T> GetSingleAsync(
//            long id,
//            CancellationToken cancellationToken = default)
//        {
//            Logger.Trace($"Start GetSingleAsync Parameter: id ({id})");
//            var result = DbSet.SingleAsync(x => x.Id == id, cancellationToken);
//            Logger.Trace($"End GetSingleAsync Parameter: id ({id})");

//            return result;
//        }
//        /// <summary>
//        /// Synchronously, query a record from the database with a condition LinQ expression.
//        /// <para>If there is no record, it will return null. </para>
//        /// <para>Example: GetSingleOrDefault(x => x.ID.ToString(CultureInfo.InvariantCulture) == "ABC");</para>
//        /// </summary>
//        public T GetSingleOrDefault(Expression<Func<T, bool>> condition = null)
//        {
//            Logger.Trace($"GetSingleOrDefault Parameter: condition ({condition})");
//            IQueryable<T> query = DbSet;
//            T result;

//            if (condition != null)
//            {
//                result = query.SingleOrDefault(condition);
//            }
//            else
//            {
//                result = query.SingleOrDefault();
//            }

//            if (result != null)
//            {
//                Logger.Trace(entityType + " entity (" + result.Id + $") is read by GetSingleOrDefault with condition ({condition}).");
//            }
//            else
//            {
//                Logger.Trace($"There is no record found in database by GetSingleOrDefault with condition ({condition}) ");
//            }

//            return result;
//        }
//        /// <summary>
//        /// Synchronously, query a record from the database by using the long.
//        /// <para>If there is no record, it will return null. </para>
//        /// </summary>
//        public T GetSingleOrDefault(long id)
//        {
//            Logger.Trace($"GetSingleOrDefault Parameter: id ({id})");
//            var result = DbSet.SingleOrDefault(x => x.Id == id);

//            if (result != null)
//            {
//                Logger.Trace(entityType + " entity (" + result.Id.ToString(CultureInfo.InvariantCulture) + ") is read by GetSingleOrDefault(id).");
//            }
//            else
//            {
//                Logger.Trace("No record is found in the database via GetSingleOrDefault(id).");
//            }
//            return result;
//        }
//        /// <summary>
//        /// Asynchronously, query a record from the database with a condition LinQ expression.
//        /// <para>If there is no record, it will return null. </para>
//        /// <para>Example: GetSingleOrDefaultAsync(x => x.ID.ToString(CultureInfo.InvariantCulture) == "ABC");</para>
//        /// </summary>
//        public Task<T> GetSingleOrDefaultAsync(
//            Expression<Func<T, bool>> condition = null,
//            CancellationToken cancellationToken = default)
//        {
//            Logger.Trace($"Start GetSingleOrDefaultAsync Parameter: condition ({condition})");
//            IQueryable<T> query = DbSet;
//            Task<T> task = null;

//            if (condition != null)
//            {
//                task = query.SingleOrDefaultAsync(condition, cancellationToken);
//            }
//            else
//            {
//                task = query.SingleOrDefaultAsync(cancellationToken);
//            }

//            Logger.Trace($"End GetSingleOrDefaultAsync Parameter: condition ({condition})");

//            return task;
//        }
//        /// <summary>
//        /// Asynchronously, query a record from the database by using the long.
//        /// <para>If there is no record, it will return null. </para>
//        /// </summary>
//        public Task<T> GetSingleOrDefaultAsync(
//            long id,
//            CancellationToken cancellationToken = default)
//        {
//            Logger.Trace($"Start GetSingleOrDefaultAsync Parameter: id ({id})");
//            var result = DbSet.SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
//            Logger.Trace($"End GetSingleOrDefaultAsync Parameter: id ({id})");

//            return result;
//        }
//        /// <summary>
//        /// Synchronously, query a list record from the database with a condition LinQ expression.
//        /// <para><paramref name="condition"/> example: GetListAsync(x => x.DataState == DataState.Inactive);</para>
//        /// <para><paramref name="orderBy"/> List of OrderBy objects that sort the returned list of records in ascending or descending order of a property denoted in OrderBy object. </para>
//        /// <para><paramref name="pageIndex"/> Indicate the page index of the returned list of record. </para>
//        /// <para><paramref name="pageSize"/> Indicate the size of each page for the returned list of record. </para>
//        /// <para><paramref name="activeDatastate"/> Return a list of record that has DataState.Active. </para>
//        /// </summary>
//        public List<T> GetList(
//            Expression<Func<T, bool>> condition = null,
//            IList<OrderBy> orderBy = null,
//            int pageIndex = 1,
//            int pageSize = 10000,
//            bool activeDatastate = true)
//        {
//            Logger.Trace($" GetList Parameter: condition ({condition}), orderBy ({orderBy}), pageIndex({pageIndex}), pageSize({pageSize}), activeDatastate({activeDatastate})");
//            List<T> resultList;
//            // checks if returned list of record needs to have DataState.active
//            if (activeDatastate)
//            {
//                resultList = GLActiveData(condition, orderBy, pageIndex, pageSize);
//            }
//            // no DataState or Datastate.Active condition
//            else
//            {
//                if (condition != null)
//                {
//                    resultList = GLOnlyCondition(condition, orderBy, pageIndex, pageSize);
//                }
//                else
//                {
//                    resultList = GLNoCondition(orderBy, pageIndex, pageSize);
//                }
//            }
//            Logger.Trace("List of " + entityType + $" entities are read by GetList with OrderBy and activeDatastate({activeDatastate}), condition({condition}.");
//            return resultList;
//        }
//        /// <summary>
//        /// Synchronously, Get List(GL) that appends active data state to <paramref name="condition"/> with orderby and pagination parameters.
//        /// </summary>
//        /// <para><paramref name="condition"/> example: GetList(x => x.DataState == DataState.Inactive);</para>
//        /// <para><paramref name="orderBy"/> List of OrderBy objects that sort the returned list of records in ascending or descending order of a property denoted in OrderBy object. </para>
//        /// <para><paramref name="pageIndex"/> Indicate the page index of the returned list of record. </para>
//        /// <para><paramref name="pageSize"/> Indicate the size of each page for the returned list of record. </para>
//        private List<T> GLActiveData(
//            Expression<Func<T, bool>> condition = null,
//            IList<OrderBy> orderBy = null,
//            int pageIndex = 1,
//            int pageSize = 10000)
//        {
//            Logger.Trace($"Executing ({entityType}) GLActiveData...");
//            List<T> resultList;
//            // Create predicate that selects record with Datastate.Active
//            var predicate = PredicateBuilder.New<T>(x => x.DataState == DataState.Active);

//            if (condition != null)
//            {
//                // AND condition with Datastate.Active predicate
//                predicate.And(condition);
//            }
//            if (orderBy != null && orderBy.Count > 0)
//            {
//                // Gets a list of record filtered by predicate and OrderBy
//                resultList = this.DbSet.Where(predicate).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
//                return resultList;
//            }
//            // Gets a list of record filtered by predicate
//            resultList = this.DbSet.Where(predicate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
//            return resultList;
//        }
//        /// <summary>
//        /// Synchronously, Get List(GL) with only <paramref name="condition"/> as predicate with orderby and pagination parameters..
//        /// </summary>
//        /// <para><paramref name="condition"/> example: GetList(x => x.DataState == DataState.Inactive);</para>
//        /// <para><paramref name="orderBy"/> List of OrderBy objects that sort the returned list of records in ascending or descending order of a property denoted in OrderBy object. </para>
//        /// <para><paramref name="pageIndex"/> Indicate the page index of the returned list of record. </para>
//        /// <para><paramref name="pageSize"/> Indicate the size of each page for the returned list of record. </para>
//        private List<T> GLOnlyCondition(
//            Expression<Func<T, bool>> condition = null,
//            IList<OrderBy> orderBy = null,
//            int pageIndex = 1,
//            int pageSize = 10000)
//        {
//            Logger.Trace($"Executing ({entityType}) GLOnlyCondition...");
//            List<T> resultList;
//            // Create predicate for passed in condition parameter
//            var predicate = PredicateBuilder.New<T>(condition);
//            if (orderBy != null && orderBy.Count > 0)
//            {
//                // Gets a list of record filtered by condition predicate and OrderBy
//                resultList = this.DbSet.Where(predicate).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
//                return resultList;
//            }
//            // Gets a list of record filtered by predicate
//            resultList = this.DbSet.Where(predicate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
//            return resultList;
//        }
//        /// <summary>
//        /// Synchronously, Get List(GL) with no condition or active data state with orderby and pagination parameters.
//        /// </summary>
//        /// <para><paramref name="orderBy"/> List of OrderBy objects that sort the returned list of records in ascending or descending order of a property denoted in OrderBy object. </para>
//        /// <para><paramref name="pageIndex"/> Indicate the page index of the returned list of record. </para>
//        /// <para><paramref name="pageSize"/> Indicate the size of each page for the returned list of record. </para>
//        private List<T> GLNoCondition(
//            IList<OrderBy> orderBy = null,
//            int pageIndex = 1,
//            int pageSize = 10000)
//        {
//            Logger.Trace($"Executing ({entityType}) GLNoCondition...");
//            List<T> resultList;

//            if (orderBy != null && orderBy.Count > 0)
//            {
//                //Gets a list of record with Orderby
//                resultList = this.DbSet.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
//                return resultList;
//            }
//            // Get a list of record
//            resultList = this.DbSet.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
//            return resultList;
//        }
//        /// <summary>
//        /// Asynchronously, query a list record from the database with a condition LinQ expression.
//        /// <para><paramref name="condition"/> example: GetListAsync(x => x.DataState == DataState.Inactive);</para>
//        /// <para><paramref name="orderBy"/> List of OrderBy objects that sort the returned list of records in ascending or descending order of a property denoted in OrderBy object. </para>
//        /// <para><paramref name="pageIndex"/> Indicate the page index of the returned list of record. </para>
//        /// <para><paramref name="pageSize"/> Indicate the size of each page for the returned list of record. </para>
//        /// <para><paramref name="activeDatastate"/> Return a list of record that has DataState.Active. </para>
//        /// </summary>
//        public async Task<List<T>> GetListAsync(
//            Expression<Func<T, bool>> condition = null,
//            IList<OrderBy> orderBy = null,
//            int pageIndex = 1,
//            int pageSize = 10000,
//            bool activeDatastate = true,
//            CancellationToken cancellationToken = default)
//        {
//            Logger.Trace($" GetListAsync Parameter: condition ({condition}), orderBy ({orderBy}), pageIndex({pageIndex}), pageSize({pageSize}), activeDatastate({activeDatastate})");
//            List<T> resultList;
//            // checks if returned list of record needs to have DataState.active
//            if (activeDatastate)
//            {
//                resultList = await GLActiveDataAsync(condition, orderBy, pageIndex, pageSize, cancellationToken).ConfigureAwait(false);
//            }
//            // no DataState or Datastate.Active condition
//            else
//            {
//                if (condition != null)
//                {
//                    resultList = await GLOnlyConditionAsync(condition, orderBy, pageIndex, pageSize, cancellationToken).ConfigureAwait(false);
//                }
//                else
//                {
//                    resultList = await GLNoConditionAsync(orderBy, pageIndex, pageSize, cancellationToken).ConfigureAwait(false);
//                }
//            }
//            Logger.Trace("List of " + entityType + $" entities are read by GetListAsync with OrderBy and activeDatastate({activeDatastate}), condition({condition}.");
//            return resultList;
//        }
//        /// <summary>
//        /// Asynchronously, Get List(GL) that appends active data state to <paramref name="condition"/> with orderby and pagination parameters.
//        /// </summary>
//        /// <para><paramref name="condition"/> example: GetListAsync(x => x.DataState == DataState.Inactive);</para>
//        /// <para><paramref name="orderBy"/> List of OrderBy objects that sort the returned list of records in ascending or descending order of a property denoted in OrderBy object. </para>
//        /// <para><paramref name="pageIndex"/> Indicate the page index of the returned list of record. </para>
//        /// <para><paramref name="pageSize"/> Indicate the size of each page for the returned list of record. </para>
//        private async Task<List<T>> GLActiveDataAsync(
//            Expression<Func<T, bool>> condition = null,
//            IList<OrderBy> orderBy = null,
//            int pageIndex = 1,
//            int pageSize = 10000,
//            CancellationToken cancellationToken = default)
//        {
//            Logger.Trace($"Executing ({entityType}) GLActiveDataAsync...");
//            List<T> resultList;
//            // Create predicate that selects record with Datastate.Active
//            var predicate = PredicateBuilder.New<T>(x => x.DataState == DataState.Active);

//            if (condition != null)
//            {
//                // AND condition with Datastate.Active predicate
//                predicate.And(condition);
//            }
//            if (orderBy != null && orderBy.Count > 0)
//            {
//                // Gets a list of record filtered by predicate and OrderBy
//                resultList = await this.DbSet.Where(predicate).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);
//                return resultList;
//            }
//            // Gets a list of record filtered by predicate
//            resultList = await this.DbSet.Where(predicate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);
//            return resultList;
//        }
//        /// <summary>
//        /// Asynchronously, Get List(GL) with only <paramref name="condition"/> as predicate with orderby and pagination parameters..
//        /// </summary>
//        /// <para><paramref name="condition"/> example: GetListAsync(x => x.DataState == DataState.Inactive);</para>
//        /// <para><paramref name="orderBy"/> List of OrderBy objects that sort the returned list of records in ascending or descending order of a property denoted in OrderBy object. </para>
//        /// <para><paramref name="pageIndex"/> Indicate the page index of the returned list of record. </para>
//        /// <para><paramref name="pageSize"/> Indicate the size of each page for the returned list of record. </para>
//        private async Task<List<T>> GLOnlyConditionAsync(
//            Expression<Func<T, bool>> condition = null,
//            IList<OrderBy> orderBy = null,
//            int pageIndex = 1,
//            int pageSize = 10000,
//            CancellationToken cancellationToken = default)
//        {
//            Logger.Trace($"Executing ({entityType}) GLOnlyConditionAsync...");
//            List<T> resultList;

//            // Create predicate for passed in condition parameter
//            var predicate = PredicateBuilder.New<T>(condition);
//            if (orderBy != null && orderBy.Count > 0)
//            {

//                // Gets a list of record filtered by condition predicate and OrderBy
//                resultList = await this.DbSet.Where(predicate).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);
//                return resultList;
//            }
//            // Gets a list of record filtered by predicate
//            resultList = await this.DbSet.Where(predicate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);
//            return resultList;
//        }
//        /// <summary>
//        /// Asynchronously, Get List(GL) with no condition or active data state with orderby and pagination parameters.
//        /// </summary>
//        /// <para><paramref name="orderBy"/> List of OrderBy objects that sort the returned list of records in ascending or descending order of a property denoted in OrderBy object. </para>
//        /// <para><paramref name="pageIndex"/> Indicate the page index of the returned list of record. </para>
//        /// <para><paramref name="pageSize"/> Indicate the size of each page for the returned list of record. </para>
//        private async Task<List<T>> GLNoConditionAsync(
//            IList<OrderBy> orderBy = null,
//            int pageIndex = 1,
//            int pageSize = 10000,
//            CancellationToken cancellationToken = default)
//        {
//            Logger.Trace($"Executing ({entityType}) GLNoConditionAsync...");
//            List<T> resultList;
//            if (orderBy != null && orderBy.Count > 0)
//            {
//                //Gets a list of record with Orderby
//                resultList = await this.DbSet.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);
//                return resultList;
//            }
//            // Get a list of record
//            resultList = await this.DbSet.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);
//            return resultList;
//        }
//        /// <summary>
//        /// Synchronously, query a list record from the database with a condition LinQ expression.
//        /// <para><paramref name="condition"/> example: GetListAsync(x => x.DataState == DataState.Inactive);</para>
//        /// </summary>
//        private List<T> GetAll(Expression<Func<T, bool>> condition = null)
//        {
//            var query = DbSet;
//            List<T> resultList;
//            if (condition != null)
//            {
//                // Gets a list of record filtered by condition
//                resultList = query.Where(condition).ToList();
//                Logger.Trace("List of " + entityType + $" entities are read by GetAll with condition({condition}.");
//                return resultList;
//            }
//            else
//            {
//                // Get a list of record
//                resultList = query.ToList();
//                Logger.Trace("List of " + entityType + $" entities are read by GetAll.");
//                return resultList;
//            }
//        }
//        /// <summary>
//        /// Checks if there is a record with <paramref name="id"/> in the database.
//        /// </summary>
//        public bool IsExists(long id)
//        {
//            return DbSet.Any(x => x.Id == id);
//        }
//        /// <summary>
//        /// Checks if there is a record that matches <paramref name="condition"/> in the database.
//        /// </summary>
//        public bool IsExists(Expression<Func<T, bool>> condition)
//        {
//            return DbSet.Any(condition);
//        }
//        #region cache synchronization methods
//        /// <summary>
//        /// Return a list of record from the database that has LastUpdate later than <paramref name="lastUpdate"/>.
//        /// <para>If <paramref name="lastUpdate"/> is null, it will return all records from the database.</para>
//        /// </summary>
//        public virtual IList<T> SynchronizePreload(DateTime? lastUpdate, Expression<Func<T, bool>> condition = null, bool activeDataState = true)
//        {
//            if (lastUpdate.HasValue)
//            {
//                lastUpdate = lastUpdate.Value.ToLocalTime();
//            }

//            if (condition != null)
//            {
//                var predicate = PredicateBuilder.New<T>(condition);

//                if (lastUpdate == null)
//                {
//                    if (activeDataState)
//                    {
//                        // first preload, will not retrieve data with DataState.Deleted
//                        predicate.And(t => t.DataState != DataState.Deleted);
//                    }
//                }
//                else
//                {
//                    // IF not first preload, must retrieve back records with DataState.Deleted for cache library to delete from cache
//                    predicate.And(t => t.CacheUpdateDT > lastUpdate);
//                }
//                return GetAll(predicate);
//            }
//            else
//            {
//                if (lastUpdate == null)
//                {
//                    if (activeDataState)
//                    {
//                        // first preload, will not retrieve data with DataState.Deleted
//                        return GetAll(t => t.DataState != DataState.Deleted);
//                    }
//                    else
//                    {
//                        // if entity does not have DataState
//                        return GetAll();
//                    }
//                }
//                else
//                {
//                    // IF not first preload, must retrieve back records with DataState.Deleted for cache library to delete from cache
//                    return GetAll(t => t.CacheUpdateDT > lastUpdate);
//                }
//            }
//        }

//        private IList<T> BuildChangedEntities(IDictionary<long, long> cacheIdVersions)
//        {
//            var dbIdVersions = this.DbSet.Select(x => KeyValuePair.Create(x.Id, x.Version)).ToDictionary(x => x.Key, x => x.Value);
//            var dbIds = dbIdVersions.Select(x => x.Key);
//            var cacheIds = cacheIdVersions.Select(x => x.Key);
//            //new added
//            var newAddIds = dbIds.Except(cacheIds);

//            //hard deleted
//            var hardDeletedIds = cacheIds.Except(dbIds);

//            //updated and soft-deleted
//            var intersectIds = dbIds.Intersect(cacheIds);
//            var updatedAndSoftDels = new List<long>();
//            foreach (var id in intersectIds)
//            {
//                //if db version larger than cache verison, then it is updated or soft-deleted
//                if (dbIdVersions.TryGetValue(id, out var dbVersion) && cacheIdVersions.TryGetValue(id, out var cacheVersion) && dbVersion > cacheVersion)
//                {
//                    updatedAndSoftDels.Add(id);
//                }
//            }
//            var newAdd_SoftDel_Updeted_Ids = newAddIds.Concat(updatedAndSoftDels);
//            var newAdd_SoftDel_Updated_Entities = this.GetEntitiesByIds(newAdd_SoftDel_Updeted_Ids);

//            //concat ench parts(newAdd, softDel, updated, hardDel)
//            var hardDeletedEntities = hardDeletedIds.Select(x =>
//            {
//                var entity = Activator.CreateInstance<T>();
//                entity.Id = x;
//                entity.DataState = DataState.Deleted;
//                return entity;
//            });
//            var result = newAdd_SoftDel_Updated_Entities.Concat(hardDeletedEntities).ToList();

//            return result;
//        }

//        /// <summary>
//        /// Although EfCore6 transform DbSet.Where(x=>ids.Contains(x.Id)) to SQL: SELECT field FROM table WHERE id IN(<=1000 ids) OR id IN() OR id IN() to avoid ORA-01795, 
//        /// another issue ORA-00913 exists if too many OR id IN ().
//        /// So we have to chunk manually 
//        /// </summary>
//        /// <param name="ids"></param>
//        /// <returns></returns>
//        private List<T> GetEntitiesByIds(IEnumerable<long> ids)
//        {
//            if (ids == null || !ids.Any())
//            {
//                return new List<T>(0);
//            }

//            var CHUNK_SIZE = 10_000;
//            var entities = new List<T>(ids.Count());
//            foreach (var item in ids.Chunk(CHUNK_SIZE))
//            {
//                entities.AddRange(this.DbSet.Where(x => item.Contains(x.Id)));
//            }

//            return entities;

//        }


//        /// <summary>
//        /// Return a list of record from the database that has Version later than <paramref name="entityVersions"/>'s version for each ID.
//        /// <para>If <paramref name="entityVersions"/> is null, no record will be returned.</para>
//        /// </summary>
//        public virtual IList<T> SynchronizeRefresh(IDictionary<long, long> entityVersions, bool activeDataState = true)
//        {
//            if ((entityVersions == null || !entityVersions.Any()) && activeDataState)
//            {
//                //retrieve all records
//                return GetAll(t => t.DataState != DataState.Deleted);
//            }

//            var entities = BuildChangedEntities(entityVersions);

//            return entities;
//        }
//        #endregion
//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//        protected virtual void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                DbContext.Dispose();
//            }
//        }

//        public T GetFirstOrDefault(Expression<Func<T, bool>> condition = null)
//        {
//            if (condition == null)
//            {
//                return DbSet.FirstOrDefault();
//            }

//            return DbSet.FirstOrDefault(condition);
//        }

//        public Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> condition = null, CancellationToken cancellationToken = default)
//        {
//            if (condition == null)
//            {
//                return DbSet.FirstOrDefaultAsync(cancellationToken);
//            }

//            return DbSet.FirstOrDefaultAsync(condition, cancellationToken);
//        }
//    }
//}
