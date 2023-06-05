using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;

namespace BiletarnicaBack.Repo
{
    public interface IDogadjajRepo
    {
        List<DogadjajEntity> GetDogadjaj();
        DogadjajEntity GetDogadjajByID(int dogadjajID);
        DogadjajEntity CreateDogadjaj(DogadjajEntity dogadjajEntity);
        DogadjajEntity GetEventByName(string nazivDogadjaja);
        void UpdateDogadjaj(DogadjajEntity dogadjajEntity);
        void DeleteDogadjaj(int dogadjajID);
        bool SaveChanges();
    }
}
