using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Models
{
    public class DogadjajUpdateDto
    {
        [Key]
        public int dogadjajID { get; set; }
        [Required]
        public string nazivDogadjaja { get; set; }
    }
}
