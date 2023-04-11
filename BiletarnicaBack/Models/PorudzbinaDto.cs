using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiletarnicaBack.Models
{
    public class PorudzbinaDto
    {
        [Key]
        public int porudzbinaID { get; set; }
        [Required]
        public DateTime datum { get; set; }
        public decimal ukupnaCena { get; set; }
        [ForeignKey("korisnik")]
        public int korisnikID { get; set; }
    }
}
