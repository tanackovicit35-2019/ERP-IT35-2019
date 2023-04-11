using AutoMapper;
using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;
using BiletarnicaBack.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;

namespace BiletarnicaBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/events")]
    public class DogadjajController : Controller
    {
       
            private readonly IMapper mapper;
            private readonly Context context;
            private readonly IDogadjajRepo dogadjajRepo;

            public DogadjajController(IMapper mapper, Context context, IDogadjajRepo dogadjajRepo)
            {
                this.mapper = mapper;
                this.context = context;
                this.dogadjajRepo= dogadjajRepo;
            }
            [AllowAnonymous]
            [HttpGet]
            [HttpHead]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public ActionResult<List<DogadjajDto>> GetDogadjaj()
            {
                var dog = dogadjajRepo.GetDogadjaj();
                if (dog == null || dog.Count == 0)
                {
                    return NoContent();
                }


                return Ok(mapper.Map<List<DogadjajDto>>(dog));
            }

        [AllowAnonymous]
        [HttpGet("{dogadjajID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<DogadjajDto> GetDogadjajByID(int dogadjajID)
        {
            
            var dogadjaj = dogadjajRepo.GetDogadjajByID(dogadjajID);
            if (dogadjaj == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<DogadjajDto>(dogadjaj));
        }

        [Authorize(Policy = "Zaposleni")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DogadjajCreateDto> CreateDogadjaj([FromBody] DogadjajCreateDto dogadjaj)
        {
            try
            {

                DogadjajEntity dog = mapper.Map<DogadjajEntity>(dogadjaj);

                DogadjajEntity confirmation = dogadjajRepo.CreateDogadjaj(dog);
                dogadjajRepo.SaveChanges();

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
        public ActionResult<DogadjajUpdateDto> UpdateDogadjaj(DogadjajEntity dogadjaj)
        {
            try
            {
                var upd = dogadjajRepo.GetDogadjajByID(dogadjaj.dogadjajID);

                if (upd == null)
                {
                    return NotFound();
                }
                DogadjajEntity dog = mapper.Map<DogadjajEntity>(dogadjaj);

                mapper.Map(dog, upd);

                dogadjajRepo.SaveChanges();

                return Ok(mapper.Map<DogadjajEntity>(dog));
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
        [HttpDelete("{dogadjajID}")]
        public IActionResult DeleteDogadjaj(int dogadjajID)
        {
            try
            {
                DogadjajEntity dog = dogadjajRepo.GetDogadjajByID(dogadjajID);
                if (dog == null)
                {
                    return NotFound();
                }

                dogadjajRepo.DeleteDogadjaj(dogadjajID);
                dogadjajRepo.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted an event");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during deleting" + ex);
            }
        }

        [HttpOptions]
        public IActionResult GetDogadjajOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
