using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiletarnicaBack.Entities
{
    public class DogadjajEntity
    {
        [Key]
        public int dogadjajID { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public string nazivDogadjaja { get; set; }
        public override string ToString()
        {
            return "Dogadjaj: { DogadjajID" + this.dogadjajID + ", Naziv dogadjaja: " + this.nazivDogadjaja + " }";
        }
    }
}
