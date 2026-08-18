using System.ComponentModel.DataAnnotations;

namespace AiChatSystem.Core.DTOS
{
    public class SignUpDto
    {
        [MaxLength(50)]
        public string UserName { get; set; }
        public string Address { get; set; }
        public string FieldOfBussiness { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(10)]
        public string Password { get; set; }

    }
}
