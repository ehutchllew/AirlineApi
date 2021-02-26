
using System.ComponentModel.DataAnnotations;
using AirlineApi.Types;

namespace AirlineApi.Models
{
    public class WebhookSubscription
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string WebhookURI { get; set; }

        [Required]
        public string Secret { get; set; }

        [Required]
        public WebhookTypes WebhookType { get; set; }

        [Required]
        public string WebhookPublisher { get; set; }
    }
}