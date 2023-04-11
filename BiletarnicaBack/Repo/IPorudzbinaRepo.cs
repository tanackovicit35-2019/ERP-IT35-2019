using BiletarnicaBack.Entities;

namespace BiletarnicaBack.Repo
{
    public interface IPorudzbinaRepo
    {
        List<PorudzbinaEntity> GetPorudzbina();
        List<PorudzbinaEntity> GetPorudzbinaKupac(int id);
        PorudzbinaEntity GetPorudzbinaByID(int porudzbinaID);
        PorudzbinaEntity CreatePorudzbina(PorudzbinaEntity porudzbinaEntity);
        void UpdatePorudzbina(PorudzbinaEntity porudzbinaEntity);
        void DeletePorudzbina(int porudzbinaID);
        bool SaveChanges();
    }
}
