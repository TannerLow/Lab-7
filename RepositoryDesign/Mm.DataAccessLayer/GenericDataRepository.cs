using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Gets all objects in that Entity class
        /// </summary>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new Entities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            }
            return list;
        }
        /// <summary>
        /// Get list of objects in an entity class
        /// </summary>
        /// <param name="where"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual IList<T> GetList(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new Entities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }
        /// <summary>
        /// Get single object in an entity class
        /// </summary>
        /// <param name="where"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual T GetSingle(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            using (var context = new Entities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            }
            return item;
        }
        /// <summary>
        /// Add to db
        /// </summary>
        /// <param name="items"></param>
        public virtual void Add(params T[] items)
        {
            Update(items);
        }
        /// <summary>
        /// Update DB
        /// </summary>
        /// <param name="items"></param>
        public virtual void Update(params T[] items)
        {
            using (var context = new Entities())
            {
                DbSet<T> dbSet = context.Set<T>();
                foreach (T item in items)
                {
                    dbSet.Add(item);
                    foreach (DbEntityEntry<IEntity> entry in context.ChangeTracker.Entries<IEntity>())
                    {
                        IEntity entity = entry.Entity;
                        entry.State = GetEntityState(entity.EntityState);
                    }
                }
                context.SaveChanges();
            }
        }
        /// <summary>
        /// Remove DB
        /// </summary>
        /// <param name="items"></param>
        public virtual void Remove(params T[] items)
        {
            Update(items);
        }
        /// <summary>
        /// Update the Entity State based on the state of the Domain Model
        /// </summary>
        /// <param name="entityState"></param>
        /// <returns></returns>
        protected static System.Data.EntityState GetEntityState(DomainModel.EntityState entityState)
        {
            switch (entityState)
            {
                case DomainModel.EntityState.Unchanged:
                    return System.Data.EntityState.Unchanged;
                case DomainModel.EntityState.Added:
                    return System.Data.EntityState.Added;
                case DomainModel.EntityState.Modified:
                    return System.Data.EntityState.Modified;
                case DomainModel.EntityState.Deleted:
                    return System.Data.EntityState.Deleted;
                default:
                    return System.Data.EntityState.Detached;
            }
        }
    }
}
