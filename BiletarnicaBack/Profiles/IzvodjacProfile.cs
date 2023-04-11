using AutoMapper;
using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;

namespace BiletarnicaBack.Profiles
{
    public class IzvodjacProfile : Profile
    {
        public IzvodjacProfile()
        {
            CreateMap<IzvodjacEntity, IzvodjacDto>();
            CreateMap<IzvodjacDto, IzvodjacEntity>();
            CreateMap<IzvodjacEntity, IzvodjacEntity>();
            CreateMap<IzvodjacCreateDto, IzvodjacEntity>();
        }
    }
}
