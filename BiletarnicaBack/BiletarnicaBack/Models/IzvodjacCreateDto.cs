using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Models
{
    public class IzvodjacCreateDto
    {
        [Required]
        public string nazivIzvodjaca { get; set; }
    }
}
