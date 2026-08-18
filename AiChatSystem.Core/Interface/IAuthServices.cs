using AiChatSystem.Core.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiChatSystem.Core.Interface
{
    public interface IAuthServices
    {
        public Task<string> Signin(LoginDto param);
        public Task<bool>SignUp(SignUpDto param);
    }
}
