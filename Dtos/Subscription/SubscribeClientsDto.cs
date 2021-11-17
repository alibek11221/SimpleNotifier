using System.Collections.Generic;

namespace Notifier.Dtos.Subscription
{
    public class SubscribeClientsDto
    {
        public int SubscriptionId { get; set; }
        public int[] ClientIds { get; set; }
    }
}