namespace BiletarnicaBack.Models
{
    public class LoginResponseDto
    {
        public int korisnikID { get; set; }
        public string role { get; set; }
        public string token { get; set; }
    }
}
