using AiChatSystem.Core.Interface;
using AiChatSystem.Core.Services;
using Microsoft.Extensions.DependencyInjection;
namespace AiChatSystem.Core
{
    public static class DependcyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            return services;
        }
    }
}
