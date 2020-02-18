using Arcade.ViewModels;
using AutoMapper;
using SharedEntities;

namespace Arcade.Configuration
{
    public class SystemSettingProfile : Profile
    {

        public SystemSettingProfile()
        {
            this.CreateMap<SystemSettingDto, SystemSettingViewModel>()
                .ForMember(p => p.Id, o => o.Ignore());
            this.CreateMap<SystemSettingViewModel, SystemSettingDto>();
        }
    }
}
