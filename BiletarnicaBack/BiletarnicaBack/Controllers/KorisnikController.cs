using AutoMapper;
using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;
using BiletarnicaBack.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiletarnicaBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/users")]
    public class KorisnikController : Controller
    {
        private readonly IMapper mapper;
        private readonly Context context;
        private readonly IKorisnikRepo korisnikRepo;

        public KorisnikController(IMapper mapper, Context context, IKorisnikRepo korisnikRepo)
        {
            this.mapper = mapper;
            this.context = context;
            this.korisnikRepo = korisnikRepo;
        }
        [Authorize(Policy = "Zaposleni")]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<KorisnikDto>> GetKorisnik()
        {
            var kor = korisnikRepo.GetKorisnik();
            if (kor == null || kor.Count == 0)
            {
                return NoContent();
            }


            return Ok(mapper.Map<List<KorisnikDto>>(kor));
        }

        [Authorize(Policy = "Zaposleni")]
        [HttpGet("{korisnikID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<KorisnikDto> GetKorisnikByID(int korisnikID)
        {

            var korisnik = korisnikRepo.GetKorisnikByID(korisnikID);
            if (korisnik == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KorisnikDto>(korisnik));
        }


        [Authorize(Policy = "Log")]
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KorisnikUpdateDto> UpdateKorisnik(KorisnikEntity korisnik)
        {
           
                try
                {
                    var upd = korisnikRepo.GetKorisnikByID(korisnik.korisnikID);
                if (upd == null)
                {
                    return NotFound();
                }
                if (korisnik.korisnikID != int.Parse(HttpContext.User.FindFirst("korisnikID").Value))
                    {
                        return Forbid();

                    }
                    KorisnikEntity k = mapper.Map<KorisnikEntity>(korisnik);
                upd.korisnikID = k.korisnikID;
                upd.korisnickoIme = k.korisnickoIme;
                upd.lozinka= k.lozinka;
                upd.ime = k.ime;
                upd.prezime = k.prezime;
                upd.email = k.email;
                upd.uloga = "kupac";

                    korisnikRepo.SaveChanges();

                    return Ok(korisnikRepo.GetKorisnikByID(upd.korisnikID));
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during updating");
                }
            
        }
        [Authorize(Policy = "Log")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{korisnikID}")]
        public IActionResult DeleteKorisnik(int korisnikID)
        {
                try
                {
                    KorisnikEntity k = korisnikRepo.GetKorisnikByID(korisnikID);
                if (k == null)
                {
                    return NotFound();
                }
                if (k.korisnikID != int.Parse(HttpContext.User.FindFirst("korisnikID").Value))
                    {
                        return Forbid();

                    }
                

                    korisnikRepo.DeleteKorisnik(korisnikID);
                    korisnikRepo.SaveChanges();

                    return StatusCode(StatusCodes.Status200OK, "You have successfully deleted a user");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during deleting" + ex);
                }
            
        }

        [HttpOptions]
        public IActionResult GetKorisnikOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
