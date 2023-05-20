using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Models
{
    public class PlacanjeCreateDto
    {

        [Required]
        public DateTime datumPlacanja { get; set; }
        public string info { get; set; }
        [ForeignKey("porudzbina")]
        public int porudzbinaID { get; set; }
    }
}
