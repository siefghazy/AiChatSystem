using AiChatSystem.Core.Interfaces;
using AiChatSystem.Core.Services.SubscriptionService.Dtos;
using AiChatSystem.Core.Services.SubscriptionService.Interfaces;
using AiChatSystem.Domain.Entities;
using AiChatSystem.Domain.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiChatSystem.Core.Services.SubscriptionService
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscribeRepo _subscribeRepo;
        private readonly ILogger<SubscriptionService> _logger;
        public SubscriptionService(ISubscribeRepo subscribeRepo,ILogger<SubscriptionService>logger)
        {
            _subscribeRepo = subscribeRepo;
            _logger= logger;  
        }
        public async Task Subscribe(SubcriptionDto param,string userId)
        {
            _logger.LogInformation("---------------------------------Starting subscription process for user {UserId}-------------------------", userId);
            Subscription sub = new Subscription
            {
                UserId = userId,
                StartDate = param.SubscriptionStartDate,
                EndDate = param.SubscriptionEndDate,
                SubscriptionPlanId = param.SubscriptionPlanId,
                Status = SubscriptionStatus.Active,
                IsDeleted = false,
            };
            await _subscribeRepo.CreateSubscription(sub);
            _logger.LogInformation("---------------------------------Creating subscription for user {UserId} with plan {PlanId}-------------------------", userId, param.SubscriptionPlanId);
        }

        public Task Unsubscribe()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Subscription>> GetTheSubs()
        {
            _logger.LogInformation("---------------------------------Retrieving all subscriptions-------------------------");
            var subs = await _subscribeRepo.GetAllSubscriptions();
            _logger.LogInformation($"---------------------------------Retrieved ${subs.Count}  subscriptions-------------------------");
            return subs;
        }
        public async Task<ICollection<Subscription>> GetValidSubs()
        {
            _logger.LogInformation("---------------------------------Retrieving valid subscriptions-------------------------");
            var subs = await _subscribeRepo.GetAllSubscriptions();
            var ValidSubs= subs.Where(s => s.Status == SubscriptionStatus.Active && s.EndDate > DateTime.UtcNow).ToList();
            _logger.LogInformation($"---------------------------------Retrieved ${subs.Count()}  valid subscriptions-------------------------");
            return subs;
        }
    }
}
