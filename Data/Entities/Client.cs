using System;
using System.Collections.Generic;
using Notifier.Data.Common;

namespace Notifier.Data.Entities
{
    public class Client : IHasTimestamps
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Subscription> Subscriptions { get; set; } = new();
        public DateTime? Created { get; set; }
        public DateTime? Deleted { get; set; }
        public DateTime? Modified { get; set; }
    }
}