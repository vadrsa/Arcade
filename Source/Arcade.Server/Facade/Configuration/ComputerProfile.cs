using AutoMapper;
using BusinessEntities;
using SharedEntities;

namespace Facade.Configuration
{
    public class ComputerProfile : Profile
    {

        public ComputerProfile()
        {
            this.CreateMap<Computer, ComputerDto>();
            this.CreateMap<Computer, ComputerSessionDto>()
                .ForMember(p => p.NextAvailableTime, o => o.Ignore());
            this.CreateMap<Computer, ComputerQueueDto>()
                .ForMember(p => p.Queue, o => o.Ignore())
                .ForMember(p => p.Current, o => o.Ignore());
            this.CreateMap<ComputerDto, Computer>();
        }
    }
}
