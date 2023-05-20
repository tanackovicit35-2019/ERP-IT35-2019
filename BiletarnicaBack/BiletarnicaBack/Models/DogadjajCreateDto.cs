using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Models
{
    public class DogadjajCreateDto
    {
        [Required]
        public string nazivDogadjaja { get; set; }
    }
}
