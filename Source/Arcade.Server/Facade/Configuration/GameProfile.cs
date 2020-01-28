using AutoMapper;
using BusinessEntities;
using Common.Core;
using Microsoft.AspNetCore.Http;
using SharedEntities;
using System;
using System.IO;

namespace Facade.Configuration
{
    public class GameProfile : Profile
    {

        public GameProfile()
        {
            this.CreateMap<Game, GameDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.Path))
                .AfterMap((src, dest) =>
                {
                    if (!String.IsNullOrEmpty(dest.Image))
                        dest.Image = HttpContextExtensions.AppBaseUrl + '/' + dest.Image;
                });
            this.CreateMap<Game, GameDetailsDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.Path))
                .AfterMap((src, dest) =>
                {
                    if (!String.IsNullOrEmpty(dest.Image))
                        dest.Image = HttpContextExtensions.AppBaseUrl + '/' + dest.Image;
                });
            this.CreateMap<Game, GameUploadDto>()
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Image.Path))
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    if (!String.IsNullOrEmpty(dest.ImagePath))
                        dest.ImagePath = HttpContextExtensions.AppBaseUrl + '/' + dest.ImagePath;
                });
            this.CreateMap<GameDto, Game>();
        }
    }
}
