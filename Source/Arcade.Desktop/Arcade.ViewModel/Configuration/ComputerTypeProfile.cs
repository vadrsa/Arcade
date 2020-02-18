using Arcade.ViewModels;
using AutoMapper;
using SharedEntities;

namespace Arcade.Configuration
{
    public class ComputerTypeProfile : Profile
    {

        public ComputerTypeProfile()
        {
            this.CreateMap<ComputerTypeDto, ComputerTypeViewModel>();
            this.CreateMap<ComputerTypeViewModel, ComputerTypeDto>();

        }
    }
}
