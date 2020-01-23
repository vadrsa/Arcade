using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.ObjectManagment
{
    public interface IObjectManagementService<TList, TDetails>
    {
        Task<List<TList>> GetAll(CancellationToken token = default(CancellationToken));

        Task<TDetails> GetForUploadByID(int id, CancellationToken token = default(CancellationToken));

        Task<int> Add(TDetails details, CancellationToken token = default(CancellationToken));

        Task Update(TDetails details, CancellationToken token = default(CancellationToken));

        Task Remove(int id, CancellationToken token = default(CancellationToken));
    }
}
