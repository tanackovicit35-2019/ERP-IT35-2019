using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BiletarnicaBack.Entities
{
    public class KorisnikEntity
    {
        [Key]
        public int korisnikID { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public string korisnickoIme { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public string lozinka { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public string ime { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public string prezime { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public string email { get; set; }
        //[Required(ErrorMessage = "Mandatory")]
        public string uloga { get; set; }

        
    }
}
