using AutoMapper;
using BusinessEntities;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.Configuration
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            this.CreateMap<Session, SessionDto>();

            this.CreateMap<SessionUploadDto, Session>()
                .ForMember(p => p.Id, o => o.Ignore())
                .ForMember(p => p.Payment, o => o.Ignore())
                .ForMember(p => p.PaymentId, o => o.Ignore())
                .ForMember(p => p.State, o => o.Ignore())
                .ForMember(p => p.EndDate, o => o.Ignore())
                .ForMember(p => p.Computer, o => o.Ignore());
        }
    }
}
