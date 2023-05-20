using AutoMapper;
using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;

namespace BiletarnicaBack.Profiles
{
    public class PorudzbinaProfile : Profile
    {
        public PorudzbinaProfile()
        {
            CreateMap<PorudzbinaEntity, PorudzbinaDto>();
            CreateMap<PorudzbinaDto, PorudzbinaEntity>();
            CreateMap<PorudzbinaEntity, PorudzbinaEntity>();
            CreateMap<PorudzbinaCreateDto, PorudzbinaEntity>();
            CreateMap<KorisnikDto, PorudzbinaEntity>();
            CreateMap<PorudzbinaEntity, KorisnikDto>();
        }
    }
}
