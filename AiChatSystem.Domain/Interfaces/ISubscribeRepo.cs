using AiChatSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiChatSystem.Domain.interfaces
{
    public interface ISubscribeRepo
    {
        public Task<ICollection<Subscription>> GetAllSubscriptions();
        public Task<Subscription> GetSubscriptionByUserId(string userId);
        public Task CreateSubscription(Subscription subscription);
        public Task UpdateSubscription(Subscription subscription);
        public Task DeleteSubscription(int id);
        public Task<Subscription> GetSubscriptionById(int SubId);
    }
}
