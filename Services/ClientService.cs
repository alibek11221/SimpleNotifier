using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notifier.Data;
using Notifier.Data.Entities;

namespace Notifier.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Save(Client client, CancellationToken cancellationToken)
        {
            await _dbContext.Clients.AddAsync(client, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AssignSubscription(Client client, Subscription subscription)
        {
            client.Subscriptions.Add(subscription);
            _dbContext.Clients.Update(client);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<Client>> GetAll()
        {
            return _dbContext.Clients.ToListAsync();
        }
        
        public async Task<bool> SaveMany(List<Client> clients, CancellationToken cancellationToken)
        {
            await _dbContext.Clients.AddRangeAsync(clients, cancellationToken);
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}