
using AdInsights.ServiceLibrary.Interface;
using AdInsights.ServiceLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Load NLog Configuration
builder.Logging.ClearProviders();
builder.Host.UseNLog();



// Add services to the container.

builder.Services.AddControllers();

// Configure Authentication (JWT)
var jwtKey = builder.Configuration["Jwt:Key"];
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jwtKey"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


// Register Tenant Service
builder.Services.AddTransient<IAdInsightsService, AdInsightsService>();
builder.Services.AddSingleton<ITenantContextService, TenantContextService>();

var app = builder.Build();     

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
