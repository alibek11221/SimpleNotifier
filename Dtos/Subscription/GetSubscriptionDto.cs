using System;
using System.Collections.Generic;
using Notifier.Data.Enums;
using Notifier.Dtos.Client;
using Notifier.Dtos.SubscriptionText;

namespace Notifier.Dtos.Subscription
{
    public class GetSubscriptionDto
    {
        public string Name { get; set; }
        public DateTime? NotificationDate { get; set; }
        public int Periodicity { get; set; }
        public SubscriptionType Type { get; set; }
        public SubscriptionPolicy Policy { get; set; }
        public List<GetClientDto> Clients { get; set; }
        public GetSubscriptionTextDto Text { get; set; }
    }
}