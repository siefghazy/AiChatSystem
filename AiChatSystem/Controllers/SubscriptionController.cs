using AiChatSystem.Core.DTOS;
using AiChatSystem.Core.Interface;
using AiChatSystem.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AiChatSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IConfiguration _configuration;
       
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        [HttpPost("Subscribe")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Subscribe([FromBody] SubcriptionDto param)
        {
            if (ModelState.IsValid == false && !HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return BadRequest("Subscription Failed Or The User Maybe Unauthorized");
            }

            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = decodedToken.Claims.FirstOrDefault(x => x.Type == "TenantId")?.Value;
            await _subscriptionService.Subscribe(param, userId);
            return Ok("Subscription Success");
        }
        [HttpGet("GetAllSubscription")]
        public async Task<IActionResult> GetSubscriptions()
        {
            var subs = await _subscriptionService.GetTheSubs();
            return Ok(subs);
        }
        }
    }
