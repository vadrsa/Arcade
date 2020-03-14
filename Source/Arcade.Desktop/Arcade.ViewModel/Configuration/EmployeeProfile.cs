using Arcade.ViewModels;
using AutoMapper;
using SharedEntities;

namespace Arcade.Configuration
{
    public class EmployeeProfile : Profile
    {

        public EmployeeProfile()
        {
            this.CreateMap<EmployeeDto, EmployeeViewModel>()
                .ForMember(p => p.Id, o => o.MapFrom(d => d.UserId));
            this.CreateMap<EmployeeViewModel, EmployeeDto>()
                .ForMember(p => p.UserId, o => o.MapFrom(d => d.Id));
            this.CreateMap<EmployeeUploadDto, EmployeeViewModel>()
                .ForMember(p => p.Id, o => o.MapFrom(d => d.UserId))
                .ForMember(p => p.IsTerminated, o => o.Ignore());
            this.CreateMap<EmployeeViewModel, EmployeeUploadDto>()
                .ForMember(p => p.Password, o => o.Ignore())
                .ForMember(p => p.UserId, o => o.MapFrom(d => d.Id));
            this.CreateMap<EmployeeUploadViewModel, EmployeeDto>()
                .ForMember(p => p.UserId, o => o.MapFrom(d => d.Id))
                .ForMember(p => p.IsTerminated, o => o.Ignore());
            this.CreateMap<EmployeeDto, EmployeeUploadViewModel>()
                .ForMember(p => p.Password, o => o.Ignore())
                .ForMember(p => p.Id, o => o.MapFrom(d => d.UserId));
            this.CreateMap<EmployeeUploadViewModel, EmployeeUploadDto>()
                .ForMember(p => p.UserId, o => o.MapFrom(d => d.Id));
            this.CreateMap<ActivityDto, ActivityViewModel>();
            this.CreateMap<EmployeeReportDto, EmployeeReportDataViewModel>()
                .ForMember(p => p.Id, o => o.MapFrom(d => d.UserId))
                .ForMember(p => p.WorkedSpan, o => o.Ignore());

        }
    }
}
