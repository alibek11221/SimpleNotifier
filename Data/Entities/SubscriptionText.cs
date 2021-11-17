using System;
using System.Collections.Generic;
using Notifier.Data.Common;

namespace Notifier.Data.Entities
{
    public class SubscriptionText : IHasTimestamps
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public string Body { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Deleted { get; set; }
        public DateTime? Modified { get; set; }
    }
}