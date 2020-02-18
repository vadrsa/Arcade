using Arcade.ViewModels;
using AutoMapper;
using SharedEntities;

namespace Arcade.Configuration
{
    public class GamesProfile : Profile
    {

        public GamesProfile()
        {
            this.CreateMap<GameDto, GameViewModel>();
            this.CreateMap<GameViewModel, GameDto>();
            this.CreateMap<GameDetailsDto, GameDetailsViewModel>();
            this.CreateMap<GameUploadDto, GameUploadViewModel>();
            this.CreateMap<GameUploadViewModel, GameUploadDto>();

        }
    }
}
