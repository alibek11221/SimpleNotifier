using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notifier.Data;
using Notifier.Data.Entities;
using Notifier.Data.Enums;

namespace Notifier.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApplicationDbContext _dbContext;

        public SubscriptionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Save(Subscription subscription)
        {
            await _dbContext.Subscriptions.AddAsync(subscription);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Subscription>> GetSubscriptionToNotify(DateTime notificationDate)
        {
            return await _dbContext.Subscriptions
                .Where(s =>
                    s.NotificationDate == null && s.Created.Value.Day == notificationDate.Day
                    || s.NotificationDate.Value.Day == notificationDate.Day)
                .Include(s => s.Clients)
                .Include(s => s.Text)
                .ToListAsync();
        }
        public async Task<List<Subscription>> GetAll()
        {
            return await _dbContext.Subscriptions
                .Include(x => x.Clients)
                .ToListAsync();
        }

        public async Task<bool> SubscribeMany(int subscriptionId, int[] clientIds, CancellationToken cancellationToken)
        {
            var sub = await _dbContext.Subscriptions
                .Include(x => x.Clients)
                .FirstAsync(x => x.Id == subscriptionId, cancellationToken);
            var clients = _dbContext.Clients.Where(x => clientIds.Contains(x.Id));
            sub.Clients.AddRange(clients);
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> SetNextDate(Subscription subscription)
        {
            switch (subscription.Policy){
                case SubscriptionPolicy.Daily:
                    subscription.NotificationDate = DateTime.Today.AddDays(subscription.Periodicity);
                    break;
                case SubscriptionPolicy.Monthly:
                    subscription.NotificationDate = DateTime.Today.AddMonths(subscription.Periodicity);
                    break;
                case SubscriptionPolicy.Yearly:
                    subscription.NotificationDate = DateTime.Today.AddYears(subscription.Periodicity);
                    break;
            }
            _dbContext.Subscriptions.Update(subscription);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}