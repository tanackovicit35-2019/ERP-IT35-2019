using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Models
{
    public class KategorijaCreateDto
    {
        [Required]
        public string nazivKategorije { get; set; }
    }
}
