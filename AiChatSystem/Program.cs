using AiChatSystem.Core.Interfaces;
using AiChatSystem.Core.Services;
using AiChatSystem.Core.Services.AuthService;
using AiChatSystem.Core.Services.AuthService.Interfaces;
using AiChatSystem.Core.Services.SubscriptionService;
using AiChatSystem.Core.Services.SubscriptionService.Interfaces;
using AiChatSystem.Domain.Entities;
using AiChatSystem.Helpers;
using AiChatSystem.Infrastructure;
using AiChatSystem.Infrastructure.Repos.SubscribtionsRepo;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Principal;
using System.Text;

var builder = WebApplication.CreateBuilder(args);




#region DBcontextService
builder.Services.AddDbContext<DBcontext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("CONNECTION_STRING"));
});
builder.Services.AddIdentity<Tenant, IdentityRole>()
    .AddEntityFrameworkStores<DBcontext>()
    .AddDefaultTokenProviders();
#endregion

#region AuthenticationConfiguration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddScoped<TokenValidationHelpers>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
#endregion

#region Loggers
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
#endregion


#region Repos
builder.Services.AddScoped<ISubscribeRepo, SubscriptionRepo>();
#endregion

#region Services
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<IAuthServices, AuthService>();
#endregion


#region CloudinaryServices
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddSingleton(provider =>
{
    var config = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;

    Account account = new Account(
        config.CloudName,
        config.ApiKey,
        config.ApiSecret);

    return new Cloudinary(account);
});
#endregion

#region signalR
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:7159") 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); 
    });
});
#endregion

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.Run();
