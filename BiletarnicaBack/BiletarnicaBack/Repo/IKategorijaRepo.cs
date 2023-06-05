using BiletarnicaBack.Entities;

namespace BiletarnicaBack.Repo
{
    public interface IKategorijaRepo
    {
        List<KategorijaEntity> GetKategorija();
        KategorijaEntity GetKategorijaByID(int kategorijaID);
        List<KartaEntity> GetKategorijaByName(string nazivKategorije);
        KategorijaEntity GetKategorijaZaKartu(string nazivKategorije);
        KategorijaEntity CreateKategorija(KategorijaEntity kategorijaEntity);
        void UpdateKategorija(KategorijaEntity kategorijaEntity);
        void DeleteKategorija(int kategorijaID);
        bool SaveChanges();
    }
}
