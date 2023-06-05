using AutoMapper;
using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiletarnicaBack.Repo
{
    public class DogadjajRepo : IDogadjajRepo
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public DogadjajRepo(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<DogadjajEntity> GetDogadjaj()
        {
            return context.dogadjaj.ToList();
        }

        public DogadjajEntity GetDogadjajByID(int dogadjajID)
        {
            return context.dogadjaj.FirstOrDefault(r => r.dogadjajID == dogadjajID);
        }

        public DogadjajEntity GetEventByName(string nazivDogadjaja)
        {
            return context.dogadjaj.FirstOrDefault(a => a.nazivDogadjaja == nazivDogadjaja);
        }

        public DogadjajEntity CreateDogadjaj(DogadjajEntity dogadjajEntity)
        {
            var created = context.Add(dogadjajEntity);
            return mapper.Map<DogadjajEntity>(created.Entity);
        }

        public void UpdateDogadjaj(DogadjajEntity dogadjajEntity)
        {
            //entity framework
        }

        public void DeleteDogadjaj(int dogadjajID)
        {
            var dogadjaj = GetDogadjajByID(dogadjajID);
            context.Remove(dogadjaj);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
