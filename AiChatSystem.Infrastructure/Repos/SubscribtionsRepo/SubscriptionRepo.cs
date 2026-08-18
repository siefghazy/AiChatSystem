using AiChatSystem.Domain.Entities;
using AiChatSystem.Domain.Enums;
using AiChatSystem.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiChatSystem.Infrastructure.Repos.SubscribtionsRepo
{
    public class SubscriptionRepo : ISubscribeRepo
    {
        private readonly DBcontext _context;
        public SubscriptionRepo(DBcontext context)
        {
            _context = context;
        }
        public async Task CreateSubscription(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubscription(int id)
        {
            var sub = await GetSubscriptionById(id);
            sub.IsDeleted = true;
            await _context.SaveChangesAsync();
        }


        public async Task<Subscription> GetSubscriptionById(int SubId)
        {
            var sub = await _context.Subscriptions.FirstOrDefaultAsync(s => s.Id == SubId && s.IsDeleted==false);
            return sub;
        }

        public async Task<Subscription> GetSubscriptionByUserId(string userId)
        {
            
            var sub= await _context.Subscriptions.FirstOrDefaultAsync(s => s.UserId == userId && s.IsDeleted == false);
            return sub;
        }

        public async Task UpdateSubscription(Subscription subscription)
        {
            var sub = await GetSubscriptionById((int)subscription.Id);
            sub.StartDate = subscription.StartDate;
            sub.Status = subscription.Status;
            sub.IsDeleted = subscription.IsDeleted;
            sub.SubscriptionPlanId = subscription.SubscriptionPlanId;
            sub.Status = subscription.Status;
            sub.EndDate = subscription.EndDate;
            sub.UserId = subscription.UserId;
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Subscription>> GetAllSubscriptions()
        {
            var subs = await _context.Subscriptions.Where(s => s.IsDeleted == false).ToListAsync();
            return subs;
        }
    }
}
