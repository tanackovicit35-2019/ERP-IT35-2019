namespace BiletarnicaBack.Models
{
    public class PaymentRequestCreateDto
    {
        public decimal Amount { get; set; }
        public string UserId { get; set; }
        public List<CartItemDto> CartItems { get; set; }

    }
}
