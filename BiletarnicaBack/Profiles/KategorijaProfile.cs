using AutoMapper;
using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;

namespace BiletarnicaBack.Profiles
{
    public class KategorijaProfile : Profile
    {
        public KategorijaProfile() {
            CreateMap<KategorijaEntity, KategorijaDto>();
            CreateMap<KategorijaDto, KategorijaEntity>();
            CreateMap<KategorijaEntity, KategorijaEntity>();
            CreateMap<KategorijaCreateDto, KategorijaEntity>();
        }
    }
}
