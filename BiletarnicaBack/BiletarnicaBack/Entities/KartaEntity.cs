using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BiletarnicaBack.Models;
using System.Text.Json.Serialization;

namespace BiletarnicaBack.Entities
{
    public class KartaEntity
    {
        [Key]
        public int kartaID { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public string nazivKarte { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public int cenaKarte { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public DateTime datumOdrzavanja { get; set; }
        [ForeignKey("dogadjaj")]
        public int dogadjajID { get; set; }
        [ForeignKey("izvodjac")]
        public int izvodjacID { get; set; }
        [ForeignKey("kategorija")]
        public int kategorijaID { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public int naStanju { get; set; }
        [NotMapped]
        public IzvodjacDto izvodjacDto { get; set; }
        [NotMapped]
        public KategorijaDto kategorijaDto{ get; set;}
        [NotMapped]
        public DogadjajDto dogadjajDto { get;set; }
        [Required(ErrorMessage = "Mandatory")]
        public string slika { get; set; }
        


    }
}
