using AirlineApi.Data;
using AirlineApi.Dtos;
using AirlineApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookSubscriptionController : ControllerBase
    {
        private readonly AirlineDbContext _context;

        public WebhookSubscriptionController(AirlineDbContext context)
        {
            this._context = context;
        }


        public ActionResult<WebhookSubscriptionReadDto> CreateSubscription(WebhookSubscriptionCreateDto webhookSubscription)
        {

        }
    }
}