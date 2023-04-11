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
    [Route("/api/orderItem")]
    public class StavkaController : Controller
    {
        private readonly IMapper mapper;
        private readonly Context context;
        private readonly IStavkaRepo stavkaRepo;
        private readonly IPorudzbinaRepo porudzbinaRepo;

        public StavkaController(IMapper mapper, Context context, IStavkaRepo stavkaRepo, IPorudzbinaRepo porudzbinaRepo)
        {
            this.mapper = mapper;
            this.context = context;
            this.stavkaRepo = stavkaRepo;
            this.porudzbinaRepo = porudzbinaRepo;
        }

        [Authorize(Policy = "Zaposleni")]
        [Route("")]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StavkaPorudzbineDto>> GetStavka()
        {
            var st = stavkaRepo.GetStavka();
            if (st == null || st.Count == 0)
            {
                return NoContent();
            }


            return Ok(mapper.Map<List<StavkaPorudzbineDto>>(st));
        }
        [Authorize(Policy = "Log")]
        [Route("/api/orderItem/item")]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StavkaPorudzbineDto>> GetStavkaKupac()
        {
            int userIDs = int.Parse(HttpContext.User.FindFirst("korisnikID").Value);
            if (userIDs == null)
            {
                return Forbid();

            }
            var st = stavkaRepo.GetStavkaKupac(userIDs);
            if (st == null || st.Count == 0)
            {
                return NoContent();
            }


            return Ok(mapper.Map<List<StavkaPorudzbineDto>>(st));
        }

        [Authorize(Policy = "Log")]
        [HttpGet("{stavkaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<StavkaPorudzbineDto> GetStavkaByID(int stavkaID)
        {
            int userIDs = int.Parse(HttpContext.User.FindFirst("korisnikID").Value);
            if (userIDs == null)
            {
                return Forbid();

            }
           
            var stavka = stavkaRepo.GetStavkaByID(stavkaID);
            var porudzbina = porudzbinaRepo.GetPorudzbinaByID(stavka.porudzbinaID);
            if (stavka == null)
            {
                return NotFound();
            }
            if(porudzbina.korisnikID == userIDs) 
            {
                return Ok(mapper.Map<StavkaPorudzbineDto>(stavka));
            }
            return Forbid();
        }

        [Authorize(Policy = "Log")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StavkaPorudzbineCreateDto> CreateStavka([FromBody] StavkaPorudzbineCreateDto stavka)
        {
            PorudzbinaEntity por = porudzbinaRepo.GetPorudzbinaByID(stavka.porudzbinaID);
            if (por.korisnikID != int.Parse(HttpContext.User.FindFirst("korisnikID").Value))
            {
                return Forbid();

            }
            try
                {

                    StavkaPorudzbineEntity s = mapper.Map<StavkaPorudzbineEntity>(stavka);

                    StavkaPorudzbineEntity confirmation = stavkaRepo.CreateStavka(s);
                    stavkaRepo.SaveChanges();

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
        public ActionResult<StavkaPorudzbineUpdateDto> UpdateStavka(StavkaPorudzbineEntity stavka)
        {
           
                try
                {
                    StavkaPorudzbineEntity upd = stavkaRepo.GetStavkaByID(stavka.stavkaID);
                    
                if (upd == null)
                    {
                        return NotFound();
                    }
                PorudzbinaEntity por = porudzbinaRepo.GetPorudzbinaByID(stavka.porudzbinaID);
                if (por.korisnikID != int.Parse(HttpContext.User.FindFirst("korisnikID").Value))
                    {
                        return Forbid();

                    }
                StavkaPorudzbineEntity s = mapper.Map<StavkaPorudzbineEntity>(stavka);

                    mapper.Map(s, upd);

                    stavkaRepo.SaveChanges();

                    return Ok(mapper.Map<StavkaPorudzbineEntity>(s));
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
        [HttpDelete("{stavkaID}")]
        public IActionResult DeleteStavka(int stavkaID)
        {
                try
                {
                    StavkaPorudzbineEntity s = stavkaRepo.GetStavkaByID(stavkaID);
                    if (s == null)
                    {
                        return NotFound();
                    }
                    PorudzbinaEntity p = porudzbinaRepo.GetPorudzbinaByID(s.porudzbinaID);
                    if (p.korisnikID != int.Parse(HttpContext.User.FindFirst("korisnikID").Value))
                        {
                            return Forbid();
                        }

                        stavkaRepo.DeleteStavka(stavkaID);
                        stavkaRepo.SaveChanges();

                        return StatusCode(StatusCodes.Status200OK, "You have successfully deleted an order item");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during deleting" + ex);
                }
        }

        [HttpOptions]
        public IActionResult GetStavkaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
