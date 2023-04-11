﻿using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Models
{
    public class KorisnikCreateDto
    {
        [Required]
        public string korisnickoIme { get; set; }
        [Required]
        public string lozinka { get; set; }
        [Required]
        public string ime { get; set; }
        [Required]
        public string prezime { get; set; }
        [Required]
        public string email { get; set; }
        //[Required]
        //public string uloga { get; set; }
    }
}
