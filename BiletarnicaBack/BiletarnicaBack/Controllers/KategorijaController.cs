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
    [Route("/api/categories")]
    public class KategorijaController : Controller
    {

        private readonly IMapper mapper;
        private readonly Context context;
        private readonly IKategorijaRepo kategorijaRepo;

        public KategorijaController(IMapper mapper, Context context, IKategorijaRepo kategorijaRepo)
        {
            this.mapper = mapper;
            this.context = context;
            this.kategorijaRepo = kategorijaRepo;
        }


        [AllowAnonymous]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<KategorijaDto>> GetKategorija()
        {
            var kat = kategorijaRepo.GetKategorija();
            if (kat == null || kat.Count == 0)
            {
                return NoContent();
            }


            return Ok(mapper.Map<List<KategorijaDto>>(kat));
        }

        [AllowAnonymous]
        [HttpGet("{kategorijaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<KategorijaDto> GetKategorijaByID(int kategorijaID)
        {

            var kategorija = kategorijaRepo.GetKategorijaByID(kategorijaID);
            if (kategorija == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KategorijaDto>(kategorija));
        }

        [Authorize(Policy = "Zaposleni")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KategorijaCreateDto> CreateKategorija([FromBody] KategorijaCreateDto kategorija)
        {
            try
            {

                KategorijaEntity kat = mapper.Map<KategorijaEntity>(kategorija);

                KategorijaEntity confirmation = kategorijaRepo.CreateKategorija(kat);
                kategorijaRepo.SaveChanges();

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
        public ActionResult<KategorijaUpdateDto> UpdateKategorija(KategorijaEntity kategorija)
        {
            try
            {
                var upd = kategorijaRepo.GetKategorijaByID(kategorija.kategorijaID);

                if (upd == null)
                {
                    return NotFound();
                }
                KategorijaEntity kat = mapper.Map<KategorijaEntity>(kategorija);

                mapper.Map(kat, upd);

                kategorijaRepo.SaveChanges();

                return Ok(mapper.Map<KategorijaEntity>(kat));
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
        [HttpDelete("{kategorijaID}")]
        public IActionResult DeleteKategorija(int kategorijaID)
        {
            try
            {
                KategorijaEntity kat = kategorijaRepo.GetKategorijaByID(kategorijaID);
                if (kat == null)
                {
                    return NotFound();
                }

                kategorijaRepo.DeleteKategorija(kategorijaID);
                kategorijaRepo.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted a category");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during deleting" + ex);
            }
        }

        [HttpOptions]
        public IActionResult GetKategorijaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
