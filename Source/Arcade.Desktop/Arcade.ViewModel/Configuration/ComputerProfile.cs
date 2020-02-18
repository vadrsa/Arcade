using Arcade.ViewModels;
using AutoMapper;
using SharedEntities;

namespace Arcade.Configuration
{
    public class ComputerProfile : Profile
    {

        public ComputerProfile()
        {
            this.CreateMap<ComputerDto, ComputerViewModel>();
            this.CreateMap<ComputerViewModel, ComputerDto>();

            this.CreateMap<ComputerQueueDto, ComputerQueueViewModel>()
                .ForMember(p => p.NextAvailableTime, o => o.Ignore())
                .ForMember(p => p.PotentialProblemWithQueue, o => o.Ignore());
            this.CreateMap<ComputerQueueViewModel, ComputerQueueDto>();

        }
    }
}
