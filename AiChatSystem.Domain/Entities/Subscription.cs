using AiChatSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiChatSystem.Domain.Entities
{
    public class Subscription
    {
        public long Id { get; set; }

        public string UserId { get; set; }
        public Tenant User { get; set; }

        public int SubscriptionPlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public SubscriptionStatus Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
