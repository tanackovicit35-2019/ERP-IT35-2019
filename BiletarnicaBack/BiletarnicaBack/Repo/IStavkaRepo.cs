using BiletarnicaBack.Entities;

namespace BiletarnicaBack.Repo
{
    public interface IStavkaRepo
    {
        List<StavkaPorudzbineEntity> GetStavka();
        List<StavkaPorudzbineEntity> GetStavkaKupac(int id);
        StavkaPorudzbineEntity GetStavkaByID(int stavkaID);
        StavkaPorudzbineEntity CreateStavka(StavkaPorudzbineEntity stavkaEntity);
        void UpdateStavka(StavkaPorudzbineEntity stavkaEntity);
        void DeleteStavka(int stavkaID);
        bool SaveChanges();
    }
}
