using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Base class for all repositories that 
    /// implements Insert, Update, Remove and Select operations for Linq2DB
    /// inteneded to be instanciated for every request.
    /// </summary>
    /// <typeparam name="T">The entity class/DB model that this repository works with</typeparam>
    /// <typeparam name="R">The repository class that inherits RepositoryBase. Example usage: public class ProductRepository&lt;ProductModel, ProductRepository&gt;</typeparam>
    public abstract class SimpleRepositoryBase<T, K, R> : RepositoryBase<T, R>, IRepository<T, K, R>
        where T : IDEntityBase<K>
        where R : class
    {
        public SimpleRepositoryBase(DataConnection context) : base(context)
        {
        }

        /// <summary>
        /// Updates the object in the table.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int Update(T obj)
        {
            return Context.Update(obj);
        }

        /// <summary>
        /// Updates the object in the table asynchronously.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(T obj, CancellationToken token = new CancellationToken())
        {
            return await Context.UpdateAsync(obj);
        }

        /// <summary>
        /// Removes the object from the table.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int Remove(T obj)
        {
            return Context.Delete(obj);
        }

        /// <summary>
        /// Removes the object from the table asynchronously.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<int> RemoveAsync(T obj, CancellationToken token = new CancellationToken())
        {
            return await Context.DeleteAsync(obj);
        }

        /// <summary>
        /// Get the entity with the specified id.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T FindByID(K key)
        {
            return ExecuteSelect(t => t.Where(o => key.Equals(o.ID)), Context).Single();
        }

        /// <summary>
        /// Get the entity with the specified id asynchrnously.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<T> FindByIDAsync(K key, CancellationToken token = new CancellationToken())
        {
            return (await ExecuteSelectAsync(t => t.Where(o => key.Equals(o.ID)), Context)).Single();
        }

        /// <summary>
        /// Get all entites from the table.
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetAll()
        {
            return ExecuteSelect(t => t, Context).ToList();
        }

        /// <summary>
        /// Get all entites from the table asynchrnously.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetAllAsync(CancellationToken token = new CancellationToken())
        {
            return (await ExecuteSelectAsync(t => t, Context)).ToList();
        }

        /// <summary>
        /// Find by predicate.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual List<T> Find(Func<T, bool> where)
        {
            return ExecuteSelect(t => t.Where(where), Context).ToList();
        }

        /// <summary>
        /// Find by predicate asynchronously.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAsync(Func<T, bool> where, CancellationToken token = new CancellationToken())
        {
            return (await ExecuteSelectAsync(t => t.Where(where), Context)).ToList();
        }

        /// <summary>
        /// Insert the object into the table
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual T Insert(T obj)
        {
            obj.ID = (K)Context.InsertWithIdentity(obj);
            return obj;
        }

        /// <summary>
        /// Insert the object into the table asynchronously.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<T> InsertAsync(T obj, CancellationToken token = new CancellationToken())
        {
            object indentity = await Context.InsertWithIdentityAsync(obj);
            if (typeof(K) == typeof(int))
            {
                obj.ID = (K)(object)Convert.ToInt32(indentity);
            }
            else
                obj.ID = (K)indentity;

            return obj;
        }
    }
}
