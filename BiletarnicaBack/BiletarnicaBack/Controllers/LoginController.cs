using AutoMapper;
using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;
using BiletarnicaBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiletarnicaBack.Controllers
{
    [ApiController]
    [Route("/api")]
    public class LoginController : Controller
    {

        private readonly IMapper mapper;
        private readonly Context context;
        private readonly IKorisnikService korisnikService;

        public LoginController(IMapper mapper, Context context, IKorisnikService korisnikService)
        {
            this.mapper = mapper;
            this.context = context;
            this.korisnikService = korisnikService;
        }

        [AllowAnonymous]
        [HttpPost("/register")]
        public IActionResult Register([FromBody] KorisnikCreateDto korisnik)
        {
            var userExists = context.korisnik.SingleOrDefault(u => u.email == korisnik.email);
            if (userExists != null) {
                return StatusCode(StatusCodes.Status409Conflict, "There is already a user with this email address");
            }

            var newU = new KorisnikEntity()
            {
                uloga = "kupac",
                email = korisnik.email,
                korisnickoIme = korisnik.korisnickoIme,
                lozinka = korisnik.lozinka,
                ime = korisnik.ime,
                prezime = korisnik.prezime
            };

            context.Add(newU);

            if (context.SaveChanges() < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var jwt = korisnikService.Authentication(newU);

            if(jwt == null) 
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return StatusCode(StatusCodes.Status200OK, new { tokenRole = jwt });
        }


        [AllowAnonymous]
        [HttpPost("/login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (loginDto.username == "" | loginDto.lozinka == "")
            {
                return StatusCode(StatusCodes.Status400BadRequest, "You have to fill in all fields");
            }

            var user = context.korisnik.SingleOrDefault(u => u.korisnickoIme == loginDto.username &&
            u.lozinka == loginDto.lozinka);

            var jwt = korisnikService.Authentication(user);

            if(jwt == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Wrong credentials");
            }

            return StatusCode(StatusCodes.Status200OK, new { response = jwt });
        }
    }
}
