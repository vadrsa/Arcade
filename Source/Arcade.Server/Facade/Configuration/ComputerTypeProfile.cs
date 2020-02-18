using AutoMapper;
using BusinessEntities;
using SharedEntities;

namespace Facade.Configuration
{
    public class ComputerTypeProfile : Profile
    {

        public ComputerTypeProfile()
        {
            this.CreateMap<ComputerType, ComputerTypeDto>();
            this.CreateMap<ComputerTypeDto, ComputerType>();
        }
    }
}
