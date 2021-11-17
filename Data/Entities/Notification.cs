using System;
using Notifier.Data.Common;
using Notifier.Data.Enums;

namespace Notifier.Data.Entities
{
    public class Notification : IHasTimestamps
    {
        public int Id { get; set; }
        public bool IsPeriodical { get; set; }
        public int? SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public NotificationStatus Status { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Deleted { get; set; }
        public DateTime? Modified { get; set; }
    }
}