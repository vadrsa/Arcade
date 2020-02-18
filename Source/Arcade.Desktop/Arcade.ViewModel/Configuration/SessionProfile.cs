using Arcade.ViewModels;
using AutoMapper;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Arcade.Configuration
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            this.CreateMap<SessionUploadViewModel, SessionUploadDto>();
            this.CreateMap<SessionUploadDto, SessionUploadViewModel>()
                .ForMember(p => p.Type, o => o.Ignore());

            this.CreateMap<SessionDto, SessionViewModel>();
            this.CreateMap<SessionViewModel, SessionDto>()
                .ForMember(p => p.Payment, o => o.Ignore())
                .ForMember(p => p.State, o => o.Ignore())
                .ForMember(p => p.Computer, o => o.Ignore());
        }
    }
}
