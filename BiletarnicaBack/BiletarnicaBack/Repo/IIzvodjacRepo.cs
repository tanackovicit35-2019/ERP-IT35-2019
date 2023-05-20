using BiletarnicaBack.Entities;

namespace BiletarnicaBack.Repo
{
    public interface IIzvodjacRepo
    {
        List<IzvodjacEntity> GetIzvodjac();
        IzvodjacEntity GetIzvodjacByID(int izvodjacID);
        IzvodjacEntity CreateIzvodjac(IzvodjacEntity izvodjacEntity);
        void UpdateIzvodjac(IzvodjacEntity izvodjacEntity);
        void DeleteIzvodjac(int izvodjacID);
        bool SaveChanges();
    }
}
