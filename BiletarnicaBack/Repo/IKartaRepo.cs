using BiletarnicaBack.Entities;

namespace BiletarnicaBack.Repo
{
    public interface IKartaRepo
    {
        List<KartaEntity> GetKarta();
        KartaEntity GetKartaByID(int kartaID);
        KartaEntity CreateKarta(KartaEntity kartaEntity);
        void UpdateKarta(KartaEntity kartaEntity);
        void DeleteKarta(int kartaID);
        bool SaveChanges();
    }
}
