using System;
using System.Collections.Generic;
using Notifier.Data.Common;
using Notifier.Data.Enums;

namespace Notifier.Data.Entities
{
    public class Subscription : IHasTimestamps
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? NotificationDate { get; set; }
        public int Periodicity { get; set; }
        public SubscriptionType Type { get; set; }
        public SubscriptionPolicy Policy { get; set; }
        public SubscriptionText Text { get; set; }
        public List<Client> Clients { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Deleted { get; set; }
        public DateTime? Modified { get; set; }
    }
}