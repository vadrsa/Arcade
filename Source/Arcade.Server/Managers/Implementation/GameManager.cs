using AutoMapper;
using BusinessEntities;
using Common.Faults;
using Common.ResponseHandling;
using DataAccess;
using Facade.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Managers.Implementation
{
    public class GameManager : ManagerBase<ArcadeContext>, IGameManager
    {

        public GameManager(IServiceProvider provider) : base(provider)
        {
        }

        public async Task<List<GameDto>> GetAll()
        {
            return await Context.Games.Include(g => g.Image).Select(g => Mapper.Map<GameDto>(g)).ToAsyncEnumerable().ToList();
        }

        public async Task<GameDetailsDto> GetById(string id)
        {
            return Mapper.Map<GameDetailsDto>(await Context.Games.Include(p => p.Image).SingleAsync(p => p.Id == id));
        }


        public async Task<GameUploadDto> GetForUpload(string id)
        {
            return Mapper.Map<GameUploadDto>(await Context.Games.Include(p => p.Image).SingleAsync(p => p.Id == id));
        }

        private GameDto ToDto(Game game)
        {
            return new GameDto { Id = game.Id, Name = game.Name, Image = game.Image?.Path };
        }

        public async Task<GameDto> AddAsync(GameUploadDto game, CancellationToken token = default)
        {
            Game real = new Game { Name = game.Name, Category = game.Category, AgeLimit = game.AgeLimit };
            if (game.Image != null)
            {
                var image = await ServiceProvider.GetService<IImageManager>().InsertBytesAsync(game.Image);
                real.Image = image;
            }
            //await Context.Games.AddAsync(real);
            Context.Entry(real).State = EntityState.Added;
            return ToDto(real);
        }

        private Game ToReal(GameUploadDto game)
        {
            return new Game { Id = game.Id, Name = game.Name };
        }

        public async Task UpdateAsync(GameUploadDto game, CancellationToken token = default)
        {
            var old = Context.Games.Include(g => g.Image).Single(g=> g.Id == game.Id);
            if (game.Image != null && game.Image.Length != 0)
            {
                var image = await ServiceProvider.GetService<IImageManager>().UpdateBytesAsync(game.Image, old.Image?.Path, token);
                old.Image = image;
            }
            old.Name = game.Name;
            old.Category = game.Category;
            old.AgeLimit = game.AgeLimit;
            Context.Games.Update(old);
        }

        public async Task RemoveAsync(string id, CancellationToken token = default)
        {
            var game = Context.Games.Include(g => g.Image).FirstOrDefault(g => g.Id == id);
            if (game == null) throw new FaultException(FaultType.ResourceNotFound);
            if (game.Image != null)
                await ServiceProvider.GetService<IImageManager>().RemoveAsync(game.Image, token);
            Context.Games.Remove(game);
        }
    }
}
