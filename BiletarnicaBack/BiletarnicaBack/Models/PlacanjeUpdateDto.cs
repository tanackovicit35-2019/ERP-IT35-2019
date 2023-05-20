using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Models
{
    public class PlacanjeUpdateDto
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
