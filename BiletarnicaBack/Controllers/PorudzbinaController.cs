using AutoMapper;
using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;
using BiletarnicaBack.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace BiletarnicaBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/orders")]
    public class PorudzbinaController : Controller
    {
        private readonly IMapper mapper;
        private readonly Context context;
        private readonly IPorudzbinaRepo porudzbinaRepo;

        public PorudzbinaController(IMapper mapper, Context context, IPorudzbinaRepo porudzbinaRepo)
        {
            this.mapper = mapper;
            this.context = context;
            this.porudzbinaRepo = porudzbinaRepo;
        }
        [Authorize(Policy = "Log")]
        [Route("/api/orders/order")]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<PorudzbinaDto>> GetPorudzbinaKupac()
        {
            var userIDs = HttpContext.User.FindFirst("korisnikID").Value;
            if (!int.TryParse(userIDs, out int userID))
            {
                return BadRequest("Invalid user ID.");

            }

            var porudz = porudzbinaRepo.GetPorudzbinaKupac(userID);
            if (porudz == null || porudz.Count == 0)
            {
                return NoContent();
            }


            return Ok(mapper.Map<List<PorudzbinaDto>>(porudz));
        }
        [Authorize(Policy = "Zaposleni")]
        [Route("/api/orders")]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<PorudzbinaDto>> GetPorudzbina()
        {

            var porudz = porudzbinaRepo.GetPorudzbina();
            if (porudz == null || porudz.Count == 0)
            {
                return NoContent();
            }


            return Ok(mapper.Map<List<PorudzbinaDto>>(porudz));
        }

        [Authorize(Policy = "Zaposleni")]
        [HttpGet("{porudzbinaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<PorudzbinaDto> GetPorudzbinaByID(int porudzbinaID)
        {

            var porudzbina = porudzbinaRepo.GetPorudzbinaByID(porudzbinaID);
                if (porudzbina == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<PorudzbinaDto>(porudzbina));
            
        }

        [Authorize(Policy = "Log")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PorudzbinaCreateDto> CreatePorudzbina([FromBody] PorudzbinaCreateDto porudzbina)
        {
            PorudzbinaEntity p = mapper.Map<PorudzbinaEntity>(porudzbina);
            if (porudzbina.korisnikID != int.Parse(HttpContext.User.FindFirst("korisnikID").Value))
            {
                return Forbid();
            }
            try
                {

                    

                PorudzbinaEntity confirmation = porudzbinaRepo.CreatePorudzbina(p);
                    porudzbinaRepo.SaveChanges();

                    return StatusCode(StatusCodes.Status201Created, "Created!");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during creating" + ex);
                }
            
        }
        [Authorize(Policy = "Log")]
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PorudzbinaUpdateDto> UpdatePorudzbina(PorudzbinaEntity porudzbina)
        {
            
                try
                {
                    PorudzbinaEntity upd = porudzbinaRepo.GetPorudzbinaByID(porudzbina.porudzbinaID);

                    if (upd == null)
                    {
                        return NotFound();
                    }
                    if (porudzbina.korisnikID != int.Parse(HttpContext.User.FindFirst("korisnikID").Value))
                    {
                        return Forbid();
                    }

                    PorudzbinaEntity p = mapper.Map<PorudzbinaEntity>(porudzbina);

                        mapper.Map(p, upd);

                        porudzbinaRepo.SaveChanges();

                        return Ok(mapper.Map<PorudzbinaEntity>(p));
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
        [HttpDelete("{porudzbinaID}")]
        public IActionResult DeletePorudzbina(int porudzbinaID)
        {
                try
                {
                    PorudzbinaEntity p = porudzbinaRepo.GetPorudzbinaByID(porudzbinaID);
                    if (p == null)
                    {
                        return NotFound();
                    }
                    if(p.korisnikID != int.Parse(HttpContext.User.FindFirst("korisnikID").Value))
                    {
                        return Forbid();
                    }

                    porudzbinaRepo.DeletePorudzbina(porudzbinaID);
                    porudzbinaRepo.SaveChanges();

                    return StatusCode(StatusCodes.Status200OK, "You have successfully deleted an order");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during deleting" + ex);
                }
           
        }

        [HttpOptions]
        public IActionResult GetPorudzbinaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
