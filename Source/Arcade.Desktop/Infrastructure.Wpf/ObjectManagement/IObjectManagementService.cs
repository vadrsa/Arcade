using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.ObjectManagement
{

    public interface IObjectManagementService<TList, TDetails, K>
    {
        Task<List<TList>> GetAll(CancellationToken token = default(CancellationToken));

        Task<TDetails> GetForUploadByID(K id, CancellationToken token = default(CancellationToken));

        Task<K> Add(TDetails details, CancellationToken token = default(CancellationToken));

        Task Update(TDetails details, CancellationToken token = default(CancellationToken));

        Task Remove(K id, CancellationToken token = default(CancellationToken));
    }
}
