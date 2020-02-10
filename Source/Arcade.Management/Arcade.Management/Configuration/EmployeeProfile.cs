using Arcade.Management.ViewModels;
using AutoMapper;
using SharedEntities;

namespace Arcade.Management.Configuration
{
    public class EmployeeProfile : Profile
    {

        public EmployeeProfile()
        {
            this.CreateMap<EmployeeDto, EmployeeViewModel>()
                .ForMember(p => p.Id, o => o.MapFrom(d => d.UserId));
            this.CreateMap<EmployeeViewModel, EmployeeDto>()
                .ForMember(p => p.UserId, o => o.MapFrom(d => d.Id));
        }
    }
}
