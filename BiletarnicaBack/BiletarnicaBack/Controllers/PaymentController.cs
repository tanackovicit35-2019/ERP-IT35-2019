using BiletarnicaBack.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace BiletarnicaBack.Controllers
{
    [Route("create-checkout-session")]
    [ApiController]

    public class PaymentController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreatePaymentSession([FromBody] PaymentRequestCreateDto request)
        {
            StripeConfiguration.ApiKey = "sk_test_51NDYKMFnzJtEpK7SKaR1i3VDvlnunXYSuP4YYMCGqEZylg1EChdlDF5DZnxwIRAie0GS2gDOmLADXj3IHCpoRnMg00pCbe3xCi";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = "http://localhost:3000/success",
                CancelUrl = "http://localhost:3000/cancel",
                ClientReferenceId = request.UserId
            };
            foreach (var product in request.CartItems)
            {
                
                
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = (long)(product.price+(0.2*product.price)), // Convert amount to cents
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.nazivKarte
                        }
                    },
                    Quantity = product.quantity
                });
            }
            var service = new SessionService();
            var session = service.Create(options);

            return Ok(new { sessionId = session.Id });
        }
    }
}
