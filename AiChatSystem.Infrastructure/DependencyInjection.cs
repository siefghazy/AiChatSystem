using AiChatSystem.Domain.interfaces;
using AiChatSystem.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AiChatSystem.Infrastructure
{
    public static class DependencyInjection
    {
       
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var CONNECTION_STRING = services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetConnectionString("CONNECTION_STRING");
            services.AddDbContext<DBcontext>(options =>
            {
                options.UseNpgsql(CONNECTION_STRING);
            });
            services.AddScoped<ISubscribeRepo, SubscriptionRepo>();
            return services;
        }

    }
}
