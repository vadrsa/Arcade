using Common.Core;
using Facade.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ApiControllerBase
    {
        public GamesController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<GameDto>> Get()
        {
            return await ServiceProvider.GetService<IGameManager>().GetAll();
        }

        [HttpGet("{id}")]
        public async Task<GameDetailsDto> GetById(string id)
        {
            return await ServiceProvider.GetService<IGameManager>().GetById(id);
        }

        [HttpGet("forupload/{id}")]
        [Authorize]
        public async Task<GameUploadDto> GetForUplaod(string id)
        {
            return await ServiceProvider.GetService<IGameManager>().GetForUpload(id);
        }

        [HttpPost]
        [Authorize]
        public async Task<GameDto> Post([FromBody] GameUploadDto game)
        {
            return await ServiceProvider.GetService<IGameManager>().AddAsync(game);
        }

        [HttpPut]
        [Authorize]
        public async Task Put([FromBody] GameUploadDto game)
        {
            await ServiceProvider.GetService<IGameManager>().UpdateAsync(game);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task Delete(string id)
        {
            await ServiceProvider.GetService<IGameManager>().RemoveAsync(id);
        }
    }
}
