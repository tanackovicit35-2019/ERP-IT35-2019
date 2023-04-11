using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;

namespace BiletarnicaBack.Repo
{
    public interface IDogadjajRepo
    {
        List<DogadjajEntity> GetDogadjaj();
        DogadjajEntity GetDogadjajByID(int dogadjajID);
        DogadjajEntity CreateDogadjaj(DogadjajEntity dogadjajEntity);
        void UpdateDogadjaj(DogadjajEntity dogadjajEntity);
        void DeleteDogadjaj(int dogadjajID);
        bool SaveChanges();
    }
}
