using Flurl.Http;
using Infrastructure.Api;
using SharedEntities;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace GamesModule.Services
{
    class GamesService : RestConsumingServiceBase
    {
        public GamesService() : base("games", ConfigurationManager.AppSettings["ApiUrl"])
        {
        }

        public async Task<List<GameDto>> GetAll(CancellationToken token = default)
        {
            return await BuildRequest().GetJsonAsync<List<GameDto>>(token);
        }


        public async Task<GameDetailsDto> GetById(string id, CancellationToken token = default)
        {
            return await BuildRequest(id).GetJsonAsync<GameDetailsDto>(token);
        }

        public async Task<GameUploadDto> GetForUpload(string id, CancellationToken token = default)
        {
            return await BuildRequest("forupload/" + id).GetJsonAsync<GameUploadDto>(token);
        }


        public async Task<GameDto> Add(GameUploadDto game, CancellationToken token = default)
        {
            return await BuildRequest().PostJsonAsync(game, token).ReceiveJson<GameDto>();
        }

        public async Task Udpate(GameUploadDto game, CancellationToken token = default)
        {
            await BuildRequest().PutJsonAsync(game, token);
        }

        public async Task Remove(string id, CancellationToken token = default)
        {
            await BuildRequest(id).DeleteAsync(token);
        }
    }
}
