using SharedEntities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface IGameManager
    {
        Task<List<GameDto>> GetAll();
        Task<GameDetailsDto> GetById(string id);
        Task<GameUploadDto> GetForUpload(string id);
        Task<GameDto> AddAsync(GameUploadDto game, CancellationToken token = new CancellationToken());
        Task UpdateAsync(GameUploadDto game, CancellationToken token = new CancellationToken());
        Task RemoveAsync(string id, CancellationToken token = new CancellationToken());
    }
}
