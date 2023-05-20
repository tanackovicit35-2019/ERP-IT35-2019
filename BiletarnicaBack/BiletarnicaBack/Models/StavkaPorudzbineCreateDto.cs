using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Models
{
    public class StavkaPorudzbineCreateDto
    {
        [Required]
        public int kolicina { get; set; }
        [Required]
        public decimal cenaStavke { get; set; }
        [ForeignKey("karta")]
        public int kartaID { get; set; }
        [ForeignKey("porudzbina")]
        public int porudzbinaID { get; set; }

    }
}
