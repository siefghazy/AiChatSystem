using AiChatSystem.Domain.interfaces;
using AiChatSystem.Infrastructure.Repos.SubscribtionsRepo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiChatSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ISubscribeRepo, SubscriptionRepo>();
            return services;
        }

    }
}
