using AutoMapper;
using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;

namespace BiletarnicaBack.Profiles
{
    public class KorisnikProfile : Profile
    {
        public KorisnikProfile()
        {
            CreateMap<KorisnikEntity, KorisnikDto>();
            CreateMap<KorisnikDto, KorisnikEntity>();
            CreateMap<KorisnikEntity, KorisnikEntity>();
            CreateMap<KorisnikCreateDto, KorisnikEntity>();
        }
    }
}
