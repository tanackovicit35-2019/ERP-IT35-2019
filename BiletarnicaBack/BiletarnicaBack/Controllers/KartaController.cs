﻿using AutoMapper;
using BiletarnicaBack.Entities;
using BiletarnicaBack.Models;
using BiletarnicaBack.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace BiletarnicaBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/tickets")]
    public class KartaController : Controller
    {
        private readonly IMapper mapper;
        private readonly Context context;
        private readonly IKartaRepo kartaRepo;
        private readonly IKategorijaRepo kategorijaRepo;
        private readonly IIzvodjacRepo izvodjacRepo;
        private readonly IDogadjajRepo dogadjajRepo;

        public KartaController(IMapper mapper, Context context, IKartaRepo kartaRepo, IKategorijaRepo kategorijaRepo, IIzvodjacRepo izvodjacRepo, IDogadjajRepo dogadjajRepo)
        {
            this.mapper = mapper;
            this.context = context;
            this.kartaRepo = kartaRepo;
            this.kategorijaRepo = kategorijaRepo;
            this.izvodjacRepo = izvodjacRepo;
            this.dogadjajRepo = dogadjajRepo;
        }
        [AllowAnonymous]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<KartaDto>> GetKarta()
        {
            var kart = kartaRepo.GetKarta();
            if (kart == null || kart.Count == 0)
            {
                return NoContent();
            }


            return Ok(mapper.Map<List<KartaDto>>(kart));
        }



        



        [AllowAnonymous]
        [HttpGet("{kartaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<KartaDto> GetKartaByID(int kartaID)
        {

            var karta = kartaRepo.GetKartaByID(kartaID);
            var izv = izvodjacRepo.GetIzvodjacByID(karta.izvodjacID);
            var dog = dogadjajRepo.GetDogadjajByID(karta.dogadjajID);
            var kat = kategorijaRepo.GetKategorijaByID(karta.kategorijaID);

            karta.kategorijaDto = new KategorijaDto
            {
                kategorijaID=kat.kategorijaID,
                nazivKategorije=kat.nazivKategorije
            };
            karta.izvodjacDto = new IzvodjacDto
            {
                izvodjacID = izv.izvodjacID,
                nazivIzvodjaca = izv.nazivIzvodjaca
            };
            karta.dogadjajDto = new DogadjajDto
            {
                dogadjajID = dog.dogadjajID,
                nazivDogadjaja = dog.nazivDogadjaja
            };
            if (karta == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KartaDto>(karta));
        }

        [Authorize(Policy = "Zaposleni")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KartaCreateDto> CreateKarta([FromBody] KartaCreateDto karta)
        {
            try
            {

                KartaEntity k = mapper.Map<KartaEntity>(karta);

                KartaEntity confirmation = kartaRepo.CreateKarta(k);
                kartaRepo.SaveChanges();

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
        public ActionResult<KartaUpdateDto> UpdateKarta(KartaEntity karta)
        {
            try
            {
                var upd = kartaRepo.GetKartaByID(karta.kartaID);

                if (upd == null)
                {
                    return NotFound();
                }
                KartaEntity k = mapper.Map<KartaEntity>(karta);

                mapper.Map(k, upd);

                kartaRepo.SaveChanges();

                return Ok(mapper.Map<KartaEntity>(k));
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
        [HttpDelete("{kartaID}")]
        public IActionResult DeleteKarta(int kartaID)
        {
            try
            {
                KartaEntity k = kartaRepo.GetKartaByID(kartaID);
                if (k == null)
                {
                    return NotFound();
                }

                kartaRepo.DeleteKarta(kartaID);
                kartaRepo.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, "You have successfully deleted a ticket");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during deleting" + ex);
            }
        }

        [HttpOptions]
        public IActionResult GetKartaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
