using AutoMapper;
using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;

namespace BiletarnicaBack.Profiles
{
    public class StavkaProfile : Profile
    {
        public StavkaProfile() {
            CreateMap<StavkaPorudzbineEntity, StavkaPorudzbineDto>();
            CreateMap<StavkaPorudzbineDto, StavkaPorudzbineEntity>();
            CreateMap<StavkaPorudzbineEntity, StavkaPorudzbineEntity>();
            CreateMap<StavkaPorudzbineCreateDto, StavkaPorudzbineEntity>();
            CreateMap<KartaDto, StavkaPorudzbineEntity>();
            CreateMap<StavkaPorudzbineEntity, KartaDto>();
            CreateMap<PorudzbinaDto, StavkaPorudzbineEntity>();
            CreateMap<StavkaPorudzbineEntity, PorudzbinaDto>();
        }
    }
}
