using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiletarnicaBack.Models
{
    public class KartaDto
    {
        [Key]
        public int kartaID { get; set; }
        [Required]
        public DateTime datumOdrzavanja { get; set; }
        [Required]
        public int naStanju { get; set; }
        [Required]
        public int cenaKarte { get; set; }
        [ForeignKey("izvodjac")]
        public int izvodjacID { get; set; }
        [ForeignKey("kategorija")]
        public int kategorijaID { get; set; }
        [ForeignKey("dogadjaj")]
        public int dogadjajID { get; set; }
       
        
        

    }
}
