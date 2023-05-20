using BiletarnicaBack.Entities;

namespace BiletarnicaBack.Repo
{
    public interface IKorisnikRepo
    {
        List<KorisnikEntity> GetKorisnik();
        KorisnikEntity GetKorisnikByID(int korisnikID);
        void UpdateKorisnik(KorisnikEntity korisnikEntity);
        void DeleteKorisnik(int korisnikID);
        bool SaveChanges();
    }
}
