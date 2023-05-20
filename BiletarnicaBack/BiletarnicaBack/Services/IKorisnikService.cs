using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;

namespace BiletarnicaBack.Services
{
    public interface IKorisnikService
    {
        public LoginResponseDto Authentication(KorisnikEntity korisnikEntity);
    }
}
