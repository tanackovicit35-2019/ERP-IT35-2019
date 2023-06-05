using AutoMapper;
using BiletarnicaBack.Entities;

namespace BiletarnicaBack.Repo
{
    public class KategorijaRepo : IKategorijaRepo
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public KategorijaRepo(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<KategorijaEntity> GetKategorija()
        {
            return context.kategorija.ToList();
        }
        public KategorijaEntity GetKategorijaZaKartu(string nazivKategorije)
        {
            return context.kategorija.FirstOrDefault(a => a.nazivKategorije == nazivKategorije);
        }

        public KategorijaEntity GetKategorijaByID(int kategorijaID)
        {
            return context.kategorija.FirstOrDefault(r => r.kategorijaID == kategorijaID);
        }
        public List<KartaEntity> GetKategorijaByName(string nazivKategorije)
        {
           
                var query = from karta in context.karta
                            join kategorija in context.kategorija on
                            karta.kategorijaID equals kategorija.kategorijaID
                            where kategorija.nazivKategorije == nazivKategorije
                            select new KartaEntity
                            {
                                kartaID = karta.kartaID,
                                datumOdrzavanja = karta.datumOdrzavanja,
                                naStanju = karta.naStanju,
                                cenaKarte = karta.cenaKarte,
                                izvodjacID = karta.izvodjacID,
                                kategorijaID = karta.kategorijaID,
                                dogadjajID = karta.dogadjajID,
                                slika = karta.slika

                            };
                return query.ToList();
            
        }



        public KategorijaEntity CreateKategorija(KategorijaEntity kategorijaEntity)
        {
            var created = context.Add(kategorijaEntity);
            return mapper.Map<KategorijaEntity>(created.Entity);
        }

        public void UpdateKategorija(KategorijaEntity kategorijaEntity)
        {
            //entity framework
        }

        public void DeleteKategorija(int kategorijaID)
        {
            var kategorija = GetKategorijaByID(kategorijaID);
            context.Remove(kategorija);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
