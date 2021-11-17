using System;
using Notifier.Data.Enums;

namespace Notifier.Dtos.Subscription
{
    public class CreateSubscriptionDto
    {
        public string Name { get; set; }
        public DateTime? NotificationDate { get; set; }
        public int Periodicity { get; set; }
        public int TextId { get; set; }
        public SubscriptionType Type { get; set; }
        public SubscriptionPolicy Policy { get; set; }
    }
}