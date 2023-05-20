using AutoMapper;
using BiletarnicaBack.Entities;

namespace BiletarnicaBack.Repo
{
    public class KorisnikRepo : IKorisnikRepo
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public KorisnikRepo(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<KorisnikEntity> GetKorisnik()
        {
            return context.korisnik.ToList();
        }

        public KorisnikEntity GetKorisnikByID(int korisnikID)
        {
            return context.korisnik.FirstOrDefault(r => r.korisnikID == korisnikID);
        }



        public void UpdateKorisnik(KorisnikEntity korisnikEntity)
        {
            //entity framework
        }

        public void DeleteKorisnik(int korisnikID)
        {
            var korisnik = GetKorisnikByID(korisnikID);
            context.Remove(korisnik);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
