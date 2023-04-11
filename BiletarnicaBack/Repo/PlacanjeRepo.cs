using AutoMapper;
using BiletarnicaBack.Entities;

namespace BiletarnicaBack.Repo
{
    public class PlacanjeRepo : IPlacanjeRepo
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public PlacanjeRepo(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<PlacanjeEntity> GetPlacanje()
        {
            return context.placanje.ToList();
        }
        public List<PlacanjeEntity> GetPlacanjeKupac(int id)
        {
            var list = from placanje in context.placanje
                       join porudzbina in context.porudzbina on placanje.porudzbinaID equals porudzbina.porudzbinaID
                       where porudzbina.korisnikID == id
                       select new PlacanjeEntity
                       {
                           placanjeID = placanje.placanjeID,
                           datumPlacanja = placanje.datumPlacanja,
                           info = placanje.info,
                           porudzbinaID = placanje.porudzbinaID
                       };
            return list.ToList();
        }

        public PlacanjeEntity GetPlacanjeByID(int placanjeID)
        {
            return context.placanje.FirstOrDefault(r => r.placanjeID == placanjeID);
        }



        public PlacanjeEntity CreatePlacanje(PlacanjeEntity placanjeEntity)
        {
            var created = context.Add(placanjeEntity);
            return mapper.Map<PlacanjeEntity>(created.Entity);
        }

        public void UpdatePlacanje(PlacanjeEntity placanjeEntity)
        {
            //entity framework
        }

        public void DeletePlacanje(int placanjeID)
        {
            var placanje = GetPlacanjeByID(placanjeID);
            context.Remove(placanje);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
