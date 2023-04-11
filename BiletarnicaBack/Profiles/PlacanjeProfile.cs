using AutoMapper;
using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;

namespace BiletarnicaBack.Profiles
{
    public class PlacanjeProfile : Profile
    {
        public PlacanjeProfile()
        {
            CreateMap<PlacanjeEntity, PlacanjeDto>();
            CreateMap<PlacanjeDto, PlacanjeEntity>();
            CreateMap<PlacanjeEntity, PlacanjeEntity>();
            CreateMap<PlacanjeCreateDto, PlacanjeEntity>();
            CreateMap<PorudzbinaDto, PlacanjeEntity>();
            CreateMap<PlacanjeEntity, PorudzbinaDto>();
        }
    }
}
