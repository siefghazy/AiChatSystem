using AiChatSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiChatSystem.Infrastructure
{
    public class DBcontext : IdentityDbContext
    {
        public DBcontext(DbContextOptions<DBcontext> options) : base(options) { }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<SubscriptionPlan>().HasData(
                new SubscriptionPlan { Id = 1, Name = "Basic", Price = 9.99m, DurationInDays = 30, IsActive = true },
                new SubscriptionPlan{Id = 2,Name = "PRO",Price = 25,DurationInDays = 30,IsActive=true},
                new SubscriptionPlan{Id = 3,Name = "Enterprise",Price = 99.99m,DurationInDays = 30,IsActive=true},
                new SubscriptionPlan { Id = 4, Name = "Base", Price = 99.99m, DurationInDays = 30, IsActive = true }
                );
        }

    }

}
