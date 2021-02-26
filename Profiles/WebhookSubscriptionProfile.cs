// Basically a Mapper from/to dto

using AutoMapper;
using AirlineApi.Dtos;
using AirlineApi.Models;

namespace AirlineApi.Profiles
{
    public class WebhookSubscriptionProfile : Profile
    {
        public WebhookSubscriptionProfile()
        {
            // Source -> Target
            CreateMap<WebhookSubscriptionCreateDto, WebhookSubscription>();
            CreateMap<WebhookSubscription, WebhookSubscriptionReadDto>();
        }
    }
}