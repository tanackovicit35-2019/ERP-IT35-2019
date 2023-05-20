using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Models
{
    public class KategorijaUpdateDto
    {
        [Key]
        public int kategorijaID { get; set; }
        [Required]
        public string nazivKategorije { get; set; }
    }
}
