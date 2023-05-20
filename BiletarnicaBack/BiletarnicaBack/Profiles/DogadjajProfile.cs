using AutoMapper;
using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;

namespace BiletarnicaBack.Profiles
{
    public class DogadjajProfile : Profile
    {
        public DogadjajProfile()
        {
            CreateMap<DogadjajEntity, DogadjajDto>();
            CreateMap<DogadjajDto, DogadjajEntity>();
            CreateMap<DogadjajEntity, DogadjajEntity>();
            CreateMap<DogadjajCreateDto, DogadjajEntity>();

        }
    }
}
