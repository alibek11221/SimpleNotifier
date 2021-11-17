using System.Collections.Generic;
using Notifier.Dtos.Subscription;

namespace Notifier.Dtos.Client
{
    public class GetClientDto
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<GetSubscriptionDto> Subscriptions { get; set; }
    }
}