using AutoMapper;
using BiletarnicaBack.Entities;

namespace BiletarnicaBack.Repo
{
    public class IzvodjacRepo : IIzvodjacRepo
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public IzvodjacRepo(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IzvodjacEntity CreateIzvodjac(IzvodjacEntity izvodjacEntity)
        {
            var created = context.Add(izvodjacEntity);
            return mapper.Map<IzvodjacEntity>(created.Entity);
        }
        public IzvodjacEntity GetPerformerByName(string nazivIzvodjaca)
        {
            return context.izvodjac.FirstOrDefault(a => a.nazivIzvodjaca == nazivIzvodjaca);
        }

        public void DeleteIzvodjac(int izvodjacID)
        {
            var izvodjac = GetIzvodjacByID(izvodjacID);
            context.Remove(izvodjac);
        }

        public List<IzvodjacEntity> GetIzvodjac()
        {
            return context.izvodjac.ToList();
        }

        public IzvodjacEntity GetIzvodjacByID(int izvodjacID)
        {
            return context.izvodjac.FirstOrDefault(r => r.izvodjacID == izvodjacID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateIzvodjac(IzvodjacEntity izvodjacEntity)
        {
            //ne treba da se implementira
        }
    }
}
