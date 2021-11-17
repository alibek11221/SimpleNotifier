using AutoMapper;
using Notifier.Data.Entities;
using Notifier.Dtos.Client;
using Notifier.Dtos.Subscription;
using Notifier.Dtos.SubscriptionText;

namespace Notifier.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Client, GetClientDto>();
            CreateMap<CreateClientDto, Client>();
            CreateMap<Subscription, GetSubscriptionDto>();
            CreateMap<CreateSubscriptionDto, Subscription>();            
            CreateMap<SubscriptionText, GetSubscriptionTextDto>();
            CreateMap<CreateSubscriptionTextDto, SubscriptionText>();
        }
    }
}