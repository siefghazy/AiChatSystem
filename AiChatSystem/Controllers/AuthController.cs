using AiChatSystem.Core.Services.AuthService.DTOs;
using AiChatSystem.Core.Services.AuthService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AiChatSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authService;
        public AuthController(IAuthServices authServices)
        {
            _authService = authServices;
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginDto param)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest("Wrong Credentials");
            }
            var result = await _authService.Signin(param);
            if (result == "Wrong Credentials")
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto param)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest("User Creation Failed");
            }
            var result = await _authService.SignUp(param);
            if (!result)
            {
                return BadRequest("User Creation Failed");
            }
            return Ok("User Created Successfully");
        }
        }
    }
