using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Models
{
    public class PorudzbinaUpdateDto
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
