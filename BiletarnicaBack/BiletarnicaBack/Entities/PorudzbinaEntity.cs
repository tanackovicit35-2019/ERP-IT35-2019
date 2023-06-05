using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Entities
{
    public class PorudzbinaEntity
    {
        [Key]
        public int porudzbinaID { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public DateTime datum { get; set; }
        public decimal ukupnaCena { get; set; }
        [ForeignKey("korisnik")]
        public int korisnikID { get; set; }
        [NotMapped]
        public KorisnikEntity korisnikEntity { get; set; }

    }
}
