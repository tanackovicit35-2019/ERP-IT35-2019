using BiletarnicaBack.Entities;

namespace BiletarnicaBack.Repo
{
    public interface IPlacanjeRepo
    {
        List<PlacanjeEntity> GetPlacanje();
        List<PlacanjeEntity> GetPlacanjeKupac(int id);
        PlacanjeEntity GetPlacanjeByID(int placanjeID);
        PlacanjeEntity CreatePlacanje(PlacanjeEntity placanjeEntity);
        void UpdatePlacanje(PlacanjeEntity placanjeEntity);
        void DeletePlacanje(int placanjeID);
        bool SaveChanges();
    }
}
