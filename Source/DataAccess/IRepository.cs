using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository<T, K, R>
    {
        R LoadWith(Expression<Func<T, object>> exp);
        R Limit(int limit);
        R Offset(int offset);
        List<T> GetAll();
        Task<List<T>> GetAllAsync(CancellationToken token = new CancellationToken());
        T Insert(T obj);
        Task<T> InsertAsync(T obj, CancellationToken token = new CancellationToken());
        long InsertRange(List<T> obj);
        Task<long> InsertRangeAsync(List<T> obj, CancellationToken token = new CancellationToken());
        T FindByID(K ID);
        Task<T> FindByIDAsync(K ID, CancellationToken token = new CancellationToken());
        List<T> Find(Func<T, bool> predicate);
        Task<List<T>> FindAsync(Func<T, bool> predicate, CancellationToken token = new CancellationToken());
        int Remove(T obj);
        Task<int> RemoveAsync(T obj, CancellationToken token = new CancellationToken());
        int Update(T obj);
        Task<int> UpdateAsync(T obj, CancellationToken token = new CancellationToken());
    }
}
