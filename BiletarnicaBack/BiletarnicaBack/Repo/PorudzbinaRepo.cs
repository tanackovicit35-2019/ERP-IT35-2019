using AutoMapper;
using BiletarnicaBack.Entities;

namespace BiletarnicaBack.Repo
{
    public class PorudzbinaRepo : IPorudzbinaRepo
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public PorudzbinaRepo(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<PorudzbinaEntity> GetPorudzbina()
        {
            return context.porudzbina.ToList();
        }
        public List<PorudzbinaEntity> GetPorudzbinaKupac(int id)
        {
            return context.porudzbina.Where(o => o.korisnikID == id).ToList();
        }

        public PorudzbinaEntity GetPorudzbinaByID(int porudzbinaID)
        {
            return context.porudzbina.FirstOrDefault(r => r.porudzbinaID == porudzbinaID);
        }



        public PorudzbinaEntity CreatePorudzbina(PorudzbinaEntity porudzbinaEntity)
        {
            var created = context.Add(porudzbinaEntity);
            return mapper.Map<PorudzbinaEntity>(created.Entity);
        }

        public void UpdatePorudzbina(PorudzbinaEntity porudzbinaEntity)
        {
            //entity framework
        }

        public void DeletePorudzbina(int porudzbinaID)
        {
            var porudzbina = GetPorudzbinaByID(porudzbinaID);
            context.Remove(porudzbina);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
