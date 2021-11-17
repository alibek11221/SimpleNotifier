using System.Collections.Generic;
using System.Threading.Tasks;
using Notifier.Data.Entities;

namespace Notifier.Services
{
    public interface ISubscriptionTextService
    {
        Task<SubscriptionText> Save(SubscriptionText subscriptionText);
        Task<SubscriptionText> GetById(int id);
        Task<List<SubscriptionText>> Get();
        Task<List<SubscriptionText>> GetWithoutSubscriptions();
        Task<SubscriptionText> AddToSubscription(int textId, Subscription subscription);
    }
}