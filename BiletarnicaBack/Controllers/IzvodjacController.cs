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
    [Route("/api/preformers")]
    public class IzvodjacController : Controller
    {
        private readonly IMapper mapper;
        private readonly Context context;
        private readonly IIzvodjacRepo izvodjacRepo;

        public IzvodjacController(IMapper mapper, Context context, IIzvodjacRepo izvodjacRepo)
        {
            this.mapper = mapper;
            this.context = context;
            this.izvodjacRepo = izvodjacRepo;
        }


        [AllowAnonymous]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<IzvodjacDto>> GetIzvodjac()
        {
            var izv = izvodjacRepo.GetIzvodjac();
            if (izv == null || izv.Count == 0)
            {
                return NoContent();
            }


            return Ok(mapper.Map<List<IzvodjacDto>>(izv));
        }

        [AllowAnonymous]
        [HttpGet("{izvodjacID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IzvodjacDto> GetIzvodjacByID(int izvodjacID)
        {

            var izvodjac = izvodjacRepo.GetIzvodjacByID(izvodjacID);
            if (izvodjac == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<IzvodjacDto>(izvodjac));
        }

        [Authorize(Policy = "Zaposleni")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IzvodjacCreateDto> CreateIzvodjac([FromBody] IzvodjacCreateDto izvodjac)
        {
            try
            {

                IzvodjacEntity izv = mapper.Map<IzvodjacEntity>(izvodjac);

                IzvodjacEntity confirmation = izvodjacRepo.CreateIzvodjac(izv);
                izvodjacRepo.SaveChanges();

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
        public ActionResult<IzvodjacUpdateDto> UpdateIzvodjac(IzvodjacEntity izvodjac)
        {
            try
            {
                var upd = izvodjacRepo.GetIzvodjacByID(izvodjac.izvodjacID);

                if (upd == null)
                {
                    return NotFound();
                }
                IzvodjacEntity izv = mapper.Map<IzvodjacEntity>(izvodjac);

                mapper.Map(izv, upd);

                izvodjacRepo.SaveChanges();

                return Ok(mapper.Map<IzvodjacEntity>(izv));
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
        [HttpDelete("{izvodjacID}")]
        public IActionResult DeleteIzvodjac(int izvodjacID)
        {
            try
            {
                IzvodjacEntity izv = izvodjacRepo.GetIzvodjacByID(izvodjacID);
                if (izv == null)
                {
                    return NotFound();
                }

                izvodjacRepo.DeleteIzvodjac(izvodjacID);
                izvodjacRepo.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted a preformer");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during deleting" + ex);
            }
        }

        [HttpOptions]
        public IActionResult GetIzvodjacOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
