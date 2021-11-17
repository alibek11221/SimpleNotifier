using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Notifier.Data.Entities;

namespace Notifier.Services
{
    public interface ISubscriptionService
    {
        Task<bool> Save(Subscription subscription);
        Task<List<Subscription>> GetSubscriptionToNotify(DateTime notificationDate);
        Task<List<Subscription>> GetAll();

        Task<bool> SubscribeMany(int subscriptionId, int[] clientIds, CancellationToken cancellationToken);

        Task<bool> SetNextDate(Subscription subscription);
    }
}