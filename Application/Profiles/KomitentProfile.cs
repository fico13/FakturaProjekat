using Application.DTOs;
using AutoMapper;
using Domain;

namespace Application.Profiles
{
    public class KomitentProfile : Profile
    {
        public KomitentProfile()
        {
            CreateMap<KomitentEntity, KomitentDTO>();
            CreateMap<KomitentDTO, KomitentEntity>();
        }
    }
}
