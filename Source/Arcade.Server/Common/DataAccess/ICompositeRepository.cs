using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Common.DataAccess
{
    public interface ICompositeRepository<T, K1, K2>
    {
        List<T> GetAll();
        Task<List<T>> GetAllAsync(CancellationToken token = new CancellationToken());
        void Insert(T obj);
        Task InsertAsync(T obj, CancellationToken token = new CancellationToken());
        long InsertRange(List<T> obj);
        Task<long> InsertRangeAsync(List<T> obj, CancellationToken token = new CancellationToken());
        T FindByID(K1 key1, K2 key2);
        Task<T> FindByIDAsync(K1 key1, K2 key2, CancellationToken token = new CancellationToken());
        List<T> Find(Func<T, bool> predicate);
        Task<List<T>> FindAsync(Func<T, bool> predicate, CancellationToken token = new CancellationToken());
        int Remove(T obj);
        Task<int> RemoveAsync(T obj, CancellationToken token = new CancellationToken());
        int Update(T obj);
        Task<int> UpdateAsync(T obj, CancellationToken token = new CancellationToken());
    }
}
