
using System.ComponentModel.DataAnnotations;
using AirlineApi.Types;

namespace AirlineApi.Dtos
{
    public class WebhookSubscriptionCreateDto
    {
        [Required]
        public string WebhookURI { get; set; }

        [Required]
        public string WebhookType { get; set; }
    }
}