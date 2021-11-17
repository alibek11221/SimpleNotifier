using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notifier.Data;
using Notifier.Data.Entities;

namespace Notifier.Services
{
    public class SubscriptionTextService : ISubscriptionTextService
    {
        private readonly ApplicationDbContext _dbContext;

        public SubscriptionTextService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<SubscriptionText> Save(SubscriptionText subscriptionText)
        {
            await _dbContext.SubscriptionTexts.AddAsync(subscriptionText);
            await _dbContext.SaveChangesAsync();
            return subscriptionText;
        }
        public async Task<SubscriptionText> GetById(int id)
        {
            return await _dbContext.SubscriptionTexts.Include(x=>x.Subscriptions).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SubscriptionText> AddToSubscription(int textId, Subscription subscription)
        {
            var text = await GetById(textId);
            text.Subscriptions.Add(subscription);
            await _dbContext.SaveChangesAsync();
            return text;
        }
        public async Task<List<SubscriptionText>> Get()
        {
            return await _dbContext.SubscriptionTexts.Include(x=>x.Subscriptions).ToListAsync();
        }
        public async Task<List<SubscriptionText>> GetWithoutSubscriptions()
        {
            return await _dbContext.SubscriptionTexts.Where(x=>x.Subscriptions == null).ToListAsync();
        }
    }
}