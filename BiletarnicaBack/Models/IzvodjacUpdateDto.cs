using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Models
{
    public class IzvodjacUpdateDto
    {
        [Key]
        public int izvodjacID { get; set; }
        [Required]
        public string nazivIzvodjaca { get; set; }
    }
}
