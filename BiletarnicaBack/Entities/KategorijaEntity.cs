using System.ComponentModel.DataAnnotations;

namespace BiletarnicaBack.Entities
{
    public class KategorijaEntity
    {
        [Key]
        public int kategorijaID { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public string nazivKategorije { get; set; }

        public override string ToString()
        {
            return "Kategorija: { KategorijaID: " + this.kategorijaID + ", Naziv Kategorije: " + this.nazivKategorije + " }";
        }
    }
}
