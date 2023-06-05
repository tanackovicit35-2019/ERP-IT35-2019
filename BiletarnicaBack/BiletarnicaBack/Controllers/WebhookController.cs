using BiletarnicaBack.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;
using System.Buffers;
using System.IO.Pipelines;
using System.Text;

namespace BiletarnicaBack.Controllers
{
    [Route("stripe-webhook")] // Endpoint for receiving Stripe webhook events
    [ApiController]
    public class WebhookController : Controller
    {
        private readonly Context context;

        public WebhookController(Context context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> HandleWebhookEvent()
        {
            var json = await GetRequestBodyAsStringAsync(Request.Body);
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], "whsec_51fe973bd447a40c3d61f5a42c817965cb3dba0f6c1cfa375a3294e758fdc4e7");

            if (stripeEvent == null)
            {
                return BadRequest("Invalid Stripe webhook event");
            }

            // Handle the specific event types you're interested in
            switch (stripeEvent.Type)
            {
                case Events.CheckoutSessionCompleted:
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                    if (session != null)
                    {
                        await HandleCheckoutSessionCompleted(session);
                    }
                    break;

                // Add more cases for other event types you want to handle

                default:
                    break;
            }

            return Ok();
        }

        private async Task<string> GetRequestBodyAsStringAsync(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
        private async Task HandleCheckoutSessionCompleted(Stripe.Checkout.Session session)
        {
            string sessionId = session.Id;
            string customerName = session.CustomerDetails.Name;
            long totalAmount = (long)session.AmountTotal;
            string paymentMethodType = session.PaymentMethodTypes.FirstOrDefault();
            var service = new SessionService();
            var options = new SessionListLineItemsOptions
            {
                Limit = 100
            };

            var lineItems = await service.ListLineItemsAsync(sessionId, options);

            int currentUserId = int.Parse(session.ClientReferenceId);


            var o = new PorudzbinaEntity
            {
                datum = DateTime.Now,
                ukupnaCena = (decimal)(totalAmount),
                korisnikID = currentUserId,

            };
            context.porudzbina.Add(o);
            context.SaveChanges();

            o = context.porudzbina.Find(o.porudzbinaID);
            int orderid = o.porudzbinaID;

            if (lineItems != null)
            {
                foreach (var lineItem in lineItems)
                {

                    string nazivKarte = lineItem.Description;
                    int quantity = (int)lineItem.Quantity;
                    decimal price = (decimal)(lineItem.Price.UnitAmount);
                    var k = context.karta.FirstOrDefault(l => l.nazivKarte == nazivKarte);
                    if (k != null)
                    {

                        var orderItem = new StavkaPorudzbineEntity
                        {
                            kolicina = quantity,
                            kartaID = k.kartaID,
                            porudzbinaID = orderid,
                            cenaStavke = price,
                        };
                        context.stavkaPorudzbine.Add(orderItem);
                    }
                }
                context.SaveChanges();
            }
            var pla = new PlacanjeEntity
            {
                datumPlacanja = DateTime.Now,
                info = "placeno",
                porudzbinaID = orderid,

            };
            context.placanje.Add(pla);
            context.SaveChanges();
        }
    }
}

