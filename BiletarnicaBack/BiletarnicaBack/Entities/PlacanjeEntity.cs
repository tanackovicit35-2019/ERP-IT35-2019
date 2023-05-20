using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Entities
{
    public class PlacanjeEntity
    {
        [Key]
        public int placanjeID { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public DateTime datumPlacanja { get; set; }
        public string info { get; set; }
        [ForeignKey("porudzbina")]
        public int porudzbinaID { get; set; }
        [NotMapped]
        public PorudzbinaEntity porudzbinaEntity { get; set; }

        public override string ToString()
        {
            return "Placanje: { PlacanjeID: " + this.placanjeID + ", Datum Placanja: " + this.datumPlacanja +
                ", Info: " + this.info + ", PorudzbinaID: " + this.porudzbinaID + " }";
        }
    }
}
