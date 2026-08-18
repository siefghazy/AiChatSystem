using AiChatSystem.Core.DTOS;
using AiChatSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiChatSystem.Core.Interface
{
    public interface ISubscriptionService
    {
        public Task Subscribe( SubcriptionDto param,string UserId);
        public Task Unsubscribe();
        public Task<ICollection<Subscription>> GetTheSubs();
        public Task<ICollection<Subscription>>GetValidSubs();
    }
}
