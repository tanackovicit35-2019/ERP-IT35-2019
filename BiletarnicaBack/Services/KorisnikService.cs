using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BiletarnicaBack.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly IConfiguration _config;

        public KorisnikService(IConfiguration config)
        {
            _config = config;   
        }

        public LoginResponseDto Authentication(KorisnikEntity korisnikEntity)
        {
            if (korisnikEntity != null)
            {
                var response = generateJwtToken(korisnikEntity);
                return response;
            }
            return null;
        }

        private LoginResponseDto generateJwtToken(KorisnikEntity korisnik)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>();

            claims.Add(new Claim("korisnikID", korisnik.korisnikID.ToString()));

            var role = korisnik.uloga;

            claims.Add(new Claim(ClaimTypes.Role, role));

            var token = new JwtSecurityToken("BiletarnicaBack", null, claims, DateTime.Now, DateTime.Now.AddDays(3), signingCredentials);

            LoginResponseDto response = new LoginResponseDto();
            response.token = tokenHandler.WriteToken(token);
            response.role = role;
            response.korisnikID = korisnik.korisnikID;

            return response;

        }
    }
}
