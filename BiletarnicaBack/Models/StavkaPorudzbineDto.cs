using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiletarnicaBack.Models
{
    public class StavkaPorudzbineDto
    {
        [Key]
        public int stavkaID { get; set; }
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
