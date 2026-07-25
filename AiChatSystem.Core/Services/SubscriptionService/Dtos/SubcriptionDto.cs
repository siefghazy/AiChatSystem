using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiChatSystem.Core.Services.SubscriptionService.Dtos
{
    public class SubcriptionDto
    {
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public int SubscriptionPlanId { get; set; }

    }
}
