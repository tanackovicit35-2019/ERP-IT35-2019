using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Models
{
    public class StavkaPorudzbineUpdateDto
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
