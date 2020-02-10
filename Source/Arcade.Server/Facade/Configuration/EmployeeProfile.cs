using AutoMapper;
using BusinessEntities;
using SharedEntities;

namespace Facade.Configuration
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {

            this.CreateMap<EmployeeUploadDto, Employee>()
                .ForMember(p => p.Id, o => o.MapFrom(d => d.UserId))
                .ForMember(p => p.User, d => d.Ignore());

            this.CreateMap<EmployeeDto, Employee>()
                .ForMember(p => p.Id, o=> o.MapFrom(d => d.UserId))
                .ForMember(p => p.User, d => d.Ignore());

            this.CreateMap<Employee, EmployeeDto>()
                .ForMember(p => p.Role, d => d.Ignore())
                .ForMember(p => p.UserName, d => d.MapFrom(p => p.User.UserName))
                .ForMember(p => p.UserId, d => d.MapFrom(p => p.Id));

            this.CreateMap<Employee, EmployeeAddResultDto>()
                .ForMember(p => p.Role, d => d.Ignore())
                .ForMember(p => p.UserName, d => d.MapFrom(p => p.User.UserName));

        }
    }
}
