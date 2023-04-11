using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Models
{
    public class IzvodjacDto
    {
        [Key]
        public int izvodjacID { get; set; }
        [Required]
        public string nazivIzvodjaca { get; set; }
    }
}
