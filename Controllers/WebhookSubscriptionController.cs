using System;
using System.Linq;
using AirlineApi.Data;
using AirlineApi.Dtos;
using AirlineApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookSubscriptionController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        private IMapper _mapper;

        public WebhookSubscriptionController(AirlineDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet("{secret}", Name = "GetSubscriptionBySecret")]
        public ActionResult<WebhookSubscriptionReadDto> GetSubscriptionBySecret([FromQuery] string secret)
        {
            WebhookSubscription subscription = this._context.WebhoookSubsriptions.FirstOrDefault(s => s.Secret == secret);

            if (subscription == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(this._mapper.Map<WebhookSubscriptionReadDto>(subscription));
            }
        }

        [HttpPost]
        public ActionResult<WebhookSubscriptionReadDto> CreateSubscription(WebhookSubscriptionCreateDto webhookSubscriptionCreateDto)
        {
            WebhookSubscription subscription = this._context.WebhoookSubsriptions.FirstOrDefault(s => s.WebhookURI == webhookSubscriptionCreateDto.WebhookURI);

            if (subscription == null)
            {
                subscription = this._mapper.Map<WebhookSubscription>(webhookSubscriptionCreateDto);

                subscription.Secret = Guid.NewGuid().ToString();
                subscription.WebhookPublisher = "PanLatam";
                try
                {
                    this._context.WebhoookSubsriptions.Add(subscription);
                    this._context.SaveChanges();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

                WebhookSubscriptionReadDto webhookSubscriptionReadDto = this._mapper.Map<WebhookSubscriptionReadDto>(subscription);

                return CreatedAtRoute(nameof(this.GetSubscriptionBySecret), new { secret = webhookSubscriptionReadDto.Secret }, webhookSubscriptionReadDto);
            }
            else
            {
                return NoContent();
            }
        }
    }
}