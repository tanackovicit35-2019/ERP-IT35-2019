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
    [Route("/api/payments")]
    public class PlacanjeController : Controller
    {
        private readonly IMapper mapper;
        private readonly Context context;
        private readonly IPlacanjeRepo placanjeRepo;
        private readonly IPorudzbinaRepo porudzbinaRepo;

        public PlacanjeController(IMapper mapper, Context context, IPlacanjeRepo placanjeRepo, IPorudzbinaRepo porudzbinaRepo)
        {
            this.mapper = mapper;
            this.context = context;
            this.placanjeRepo = placanjeRepo;
            this.porudzbinaRepo = porudzbinaRepo;
        }
        [Authorize(Policy = "Zaposleni")]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<PlacanjeDto>> GetPlacanje()
        {
            var pla = placanjeRepo.GetPlacanje();
            if (pla == null || pla.Count == 0)
            {
                return NoContent();
            }


            return Ok(mapper.Map<List<PlacanjeDto>>(pla));
        }

        [Authorize(Policy = "Zaposleni")]
        [HttpGet("{placanjeID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<PlacanjeDto> GetPlacanjeByID(int placanjeID)
        {

            var placanje = placanjeRepo.GetPlacanjeByID(placanjeID);
            if (placanje == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PlacanjeDto>(placanje));
        }

        [Authorize(Policy = "Log")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlacanjeCreateDto> CreatePlacanje([FromBody] PlacanjeCreateDto placanje)
        {
            
            try
            {

                PlacanjeEntity p = mapper.Map<PlacanjeEntity>(placanje);
                PorudzbinaEntity por = porudzbinaRepo.GetPorudzbinaByID(placanje.porudzbinaID);

                if (por.korisnikID != int.Parse(HttpContext.User.FindFirst("korisnikID").Value))
                {
                    return Forbid();
                }

                PlacanjeEntity confirmation = placanjeRepo.CreatePlacanje(p);
                placanjeRepo.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, "Created!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during creating" + ex);
            }
        }
        [Authorize(Policy = "Zaposleni")]
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlacanjeUpdateDto> UpdatePlacanje(PlacanjeEntity placanje)
        {
            try
            {
                var upd = placanjeRepo.GetPlacanjeByID(placanje.placanjeID);

                if (upd == null)
                {
                    return NotFound();
                }
                PlacanjeEntity p = mapper.Map<PlacanjeEntity>(placanje);

                mapper.Map(p, upd);

                placanjeRepo.SaveChanges();

                return Ok(mapper.Map<PlacanjeEntity>(p));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during updating");
            }
        }
        [Authorize(Policy = "Zaposleni")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{placanjeID}")]
        public IActionResult DeletePlacanje(int placanjeID)
        {
            try
            {
                PlacanjeEntity p = placanjeRepo.GetPlacanjeByID(placanjeID);
                if (p == null)
                {
                    return NotFound();
                }

                placanjeRepo.DeletePlacanje(placanjeID);
                placanjeRepo.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted a payment");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during deleting" + ex);
            }
        }

        [HttpOptions]
        public IActionResult GetPlacanjeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
