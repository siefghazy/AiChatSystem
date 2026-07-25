using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiChatSystem.Core.Services.AuthService.DTOs
{
    public class LoginDto
    {
        [MaxLength(50)]
        public string? UserName { get; set; }
        public string password { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
    }
}
