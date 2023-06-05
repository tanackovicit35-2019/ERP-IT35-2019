using AutoMapper;
using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;

namespace BiletarnicaBack.Profiles
{
    public class KartaProfile : Profile
    {
        public KartaProfile() {
            CreateMap<KartaEntity, KartaDto>();
            CreateMap<KartaDto, KartaEntity>();
            CreateMap<KartaEntity, KartaEntity>();
            CreateMap<KartaEntity,KartaUpdateDto>();
            CreateMap<KartaUpdateDto,KartaEntity>();
            CreateMap<KartaDto, KartaDto>();
            CreateMap<KartaEntity, DogadjajDto>();
            CreateMap<DogadjajDto, KartaEntity>();
            CreateMap<KartaEntity, IzvodjacDto>();
            CreateMap<IzvodjacDto, KartaEntity>();
            CreateMap<KategorijaDto, KartaEntity>();
            CreateMap<KartaEntity, KategorijaDto>();
            CreateMap<KartaCreateDto, KartaEntity>();
            CreateMap<KategorijaEntity, KartaCreateDto>();
        }
    }
}
