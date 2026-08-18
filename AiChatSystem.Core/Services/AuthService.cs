using AiChatSystem.Core.DTOS;
using AiChatSystem.Core.Interface;
using AiChatSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AiChatSystem.Core.Services
{
    public class AuthService : IAuthServices
    {
        private readonly UserManager<Tenant> _userManager;
        private readonly IConfiguration _configuration;
        public AuthService( UserManager<Tenant> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> Signin(LoginDto param)
        {
           if(param.UserName!=null)
            {
                var user = await _userManager.FindByNameAsync(param.UserName);
                if (user != null)
                {
                    var result = await _userManager.CheckPasswordAsync(user, param.password);
                    if (result)
                    {
                        var token = GenerateToken(user);
                        return token;
                    }
                }
                return "Wrong Credientials";
            }
            else if(param.Email!=null)
            {
                var user = await _userManager.FindByEmailAsync(param.Email);
                if (user != null)
                {
                    var result = await _userManager.CheckPasswordAsync(user, param.password);
                    if (result)
                    {
                        var token= GenerateToken(user);
                        return token;
                    }
                }
                return "Wrong Credentials";
            }
            return "Wrong Credentials";
        }

        public async Task<bool> SignUp(SignUpDto param)
        {
            Tenant user = new Tenant
            {
                UserName = param.UserName,
                Email = param.Email,
                FieldOfBussiness = param.FieldOfBussiness,
                Address = param.Address,
                PhoneNumber = param.PhoneNumber,
                IsSubscribed = false
            };
            var result = await _userManager.CreateAsync(user, param.Password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
        private string GenerateToken(Tenant user)
        {
            var claims = new[]
            {
                new Claim("TenantId",user.Id.ToString()),
                new Claim("TenantUserName",user.UserName),
                new Claim("TenantEmail",user.Email),
                new Claim("IsAdmin","false"),
                new Claim("IsSubscribed",user.IsSubscribed.ToString())
            };
            //Bring The Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //Encrypt it
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //make the token
            var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"], audience: _configuration["Jwt:Audience"], claims: claims, expires: DateTime.UtcNow.AddDays(7), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
