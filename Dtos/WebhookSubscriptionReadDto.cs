
using AirlineApi.Types;

namespace AirlineApi.Dtos
{
    public class WebhookSubscriptionReadDto
    {
        public int Id { get; set; }

        public string WebhookURI { get; set; }

        public string Secret { get; set; }

        public WebhookTypes WebhookType { get; set; }

        public string WebhookPublisher { get; set; }
    }
}