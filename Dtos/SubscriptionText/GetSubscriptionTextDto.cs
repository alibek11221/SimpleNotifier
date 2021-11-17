using System.Collections.Generic;
using Notifier.Dtos.Subscription;

namespace Notifier.Dtos.SubscriptionText
{
    public class GetSubscriptionTextDto
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public List<GetSubscriptionDto> Subscriptions { get; set; }

        public override string ToString()
        {
            return Subject;
        }
    }
}