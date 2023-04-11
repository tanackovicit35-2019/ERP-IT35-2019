using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Entities
{
    public class IzvodjacEntity
    {
        [Key]
        public int izvodjacID { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public string nazivIzvodjaca { get; set; }

        public override string ToString()
        {
            return "Izvodjac: { IzvodjacID: " + this.izvodjacID + " , Naziv Izvodjaca: " + this.nazivIzvodjaca + " }";
        }
    }
}
