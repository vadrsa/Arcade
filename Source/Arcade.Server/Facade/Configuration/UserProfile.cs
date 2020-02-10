using AutoMapper;
using BusinessEntities;
using SharedEntities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.Configuration
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<UserDto, User>();
            this.CreateMap<User, UserDto>();
        }
    }
}
