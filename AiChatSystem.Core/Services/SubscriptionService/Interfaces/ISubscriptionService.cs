using AiChatSystem.Core.Services.SubscriptionService.Dtos;
using AiChatSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiChatSystem.Core.Services.SubscriptionService.Interfaces
{
    public interface ISubscriptionService
    {
        public Task Subscribe( SubcriptionDto param,string UserId);
        public Task Unsubscribe();
        public Task<ICollection<Subscription>> GetTheSubs();
        public Task<ICollection<Subscription>>GetValidSubs();
    }
}
