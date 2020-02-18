using BusinessEntities;
using SharedEntities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface ISessionManager
    {
        Task<List<ComputerQueueDto>> GetAllComputers();
        Task<ComputerQueueDto> GetComputerById(string computerId);
        Task<ComputerQueueDto> GetFullComputerById(string computerId);
        Task<SessionDto> Create(SessionUploadDto session, CancellationToken token = new CancellationToken());
        Task<SessionDto> AddToQueue(SessionUploadDto session, CancellationToken token = default);
        Task EndSession(string id);
    }
}
