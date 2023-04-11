using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiletarnicaBack.Models
{
    public class PlacanjeDto
    {
        [Key]
        public int placanjeID { get; set; }
        [Required]
        public DateTime datumPlacanja { get; set; }
        public string info { get; set; }
        [ForeignKey("porudzbina")]
        public int porudzbinaID { get; set; }
    }
}
