using AutoMapper;
using BiletarnicaBack.Entities;

namespace BiletarnicaBack.Repo
{
    public class StavkaRepo : IStavkaRepo
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public StavkaRepo(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<StavkaPorudzbineEntity> GetStavka()
        {
            return context.stavkaPorudzbine.ToList();
        }

        public List<StavkaPorudzbineEntity> GetStavkaKupac(int id)
        {
            var list = from stavkaPorudzbine in context.stavkaPorudzbine
                       join porudzbina in context.porudzbina on stavkaPorudzbine.porudzbinaID equals porudzbina.porudzbinaID
                       join korisnik in context.korisnik on porudzbina.korisnikID equals korisnik.korisnikID
                       where porudzbina.korisnikID == id
                       select new StavkaPorudzbineEntity
                       {
                           stavkaID = stavkaPorudzbine.stavkaID,
                           kolicina = stavkaPorudzbine.kolicina,
                           cenaStavke = stavkaPorudzbine.cenaStavke,
                           kartaID = stavkaPorudzbine.kartaID,
                           porudzbinaID = stavkaPorudzbine.porudzbinaID
                       };
            return list.ToList();
        }



        public StavkaPorudzbineEntity GetStavkaByID(int stavkaID)
        {
            return context.stavkaPorudzbine.FirstOrDefault(r => r.stavkaID == stavkaID);
        }



        public StavkaPorudzbineEntity CreateStavka(StavkaPorudzbineEntity stavkaEntity)
        {
            var created = context.Add(stavkaEntity);
            return mapper.Map<StavkaPorudzbineEntity>(created.Entity);
        }

        public void UpdateStavka(StavkaPorudzbineEntity stavkaEntity)
        {
            //entity framework
        }

        public void DeleteStavka(int stavkaID)
        {
            var stavka = GetStavkaByID(stavkaID);
            context.Remove(stavka);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
