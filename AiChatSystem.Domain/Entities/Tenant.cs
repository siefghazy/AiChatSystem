using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiChatSystem.Domain.Entities
{
    public class Tenant:IdentityUser
    {
        public string Address { get; set; }
        public string FieldOfBussiness { get; set; }
        public bool IsSubscribed { get; set; }
        public bool IsDeleted { get; set; }
    }
}
