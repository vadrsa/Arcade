using BusinessEntities;
using Common.Core;
using Common.Faults;
using Facade.Managers;
using Facade.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Managers.Implementation
{
    public class GameManager : ManagerBase, IGameManager
    {

        public GameManager(IServiceProvider provider) : base(provider)
        {
        }

        public async Task<List<GameDto>> GetAll()
        {
            return Mapper.Map<List<GameDto>>(await ServiceProvider.GetService<IGameRepository>().LoadWith(g => g.Image).GetAllAsync());
        }

        public async Task<GameDetailsDto> GetById(string id)
        {
            return Mapper.Map<GameDetailsDto>(await ServiceProvider.GetService<IGameRepository>().LoadWith(g => g.Image).FindByIDAsync(id));
        }


        public async Task<GameUploadDto> GetForUpload(string id)
        {
            return Mapper.Map<GameUploadDto>(await ServiceProvider.GetService<IGameRepository>().LoadWith(g => g.Image).FindByIDAsync(id));
        }

        private GameDto ToDto(Game game)
        {
            return new GameDto { Id = game.Id, Name = game.Name, Image = game.Image?.Path };
        }

        [Transaction]
        public async Task<GameDto> AddAsync(GameUploadDto game, CancellationToken token = default)
        {
            Game real = new Game { Name = game.Name, Category = game.Category, AgeLimit = game.AgeLimit };
            if (game.Image != null)
            {
                var image = await ServiceProvider.GetService<IImageManager>().InsertBytesAsync(game.Image);
                real.ImageId = image.Id;
            }

            var added = await ServiceProvider.GetService<IGameRepository>().InsertAsync(real, token);

            return Mapper.Map<GameDto>(added);
        }

        private Game ToReal(GameUploadDto game)
        {
            return new Game { Id = game.Id, Name = game.Name };
        }

        [Transaction]
        public async Task UpdateAsync(GameUploadDto game, CancellationToken token = default)
        {
            var old = await ServiceProvider.GetService<IGameRepository>().LoadWith(g => g.Image).FindByIDAsync(game.Id, token);
            if (game.Image != null && game.Image.Length != 0)
            {
                var image = await ServiceProvider.GetService<IImageManager>().UpdateBytesAsync(game.Image, old.Image?.Path, token);
                old.Image = image;
            }
            old.Name = game.Name;
            old.Category = game.Category;
            old.AgeLimit = game.AgeLimit;
            await ServiceProvider.GetService<IGameRepository>().UpdateAsync(old, token);
        }

        [Transaction]
        public async Task RemoveAsync(string id, CancellationToken token = default)
        {
            var game = await ServiceProvider.GetService<IGameRepository>().LoadWith(g => g.Image).FindByIDAsync(id, token);
            if (game == null) throw new FaultException(FaultType.ResourceNotFound);
            if (game.Image != null)
                await ServiceProvider.GetService<IImageManager>().RemoveAsync(game.Image, token);
            await ServiceProvider.GetService<IGameRepository>().RemoveAsync(game);
        }
    }
}
