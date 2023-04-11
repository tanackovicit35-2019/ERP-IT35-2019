using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Models
{
    public class KategorijaDto
    {
        [Key]
        public int kategorijaID { get; set; }
        [Required]
        public string nazivKategorije { get; set; }
    }
}
