using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Entities
{
    public class StavkaPorudzbineEntity
    {
        [Key]
        public int stavkaID { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public int kolicina { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public decimal cenaStavke { get; set; }
        [ForeignKey("karta")]
        public int kartaID { get; set; }
        [ForeignKey("porudzbina")]
        public int porudzbinaID { get; set; }
        [NotMapped]
        public KartaEntity kartaEntity { get; set; }
        [NotMapped]
        public PorudzbinaEntity porudzbinaEntity { get;set; }

        public override string ToString()
        {
            return "Stavka Porudzbine: { StavkaPorudzbineID: " + this.stavkaID + ", Kolicina: " + this.kolicina +
                ", Cena Stavke: " + this.cenaStavke + ", KartaID: " + this.kartaID + ", PorudzbinaID: " + this.porudzbinaID + "}";
        }
    }
}
