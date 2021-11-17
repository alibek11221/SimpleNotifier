using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Notifier.Data.Entities;

namespace Notifier.Services
{
    public interface IClientService
    {
        Task Save(Client client, CancellationToken cancellationToken);
        Task AssignSubscription(Client client, Subscription subscription);
        Task<List<Client>> GetAll();

        Task<bool> SaveMany(List<Client> clients, CancellationToken cancellationToken);
    }
}