
using AdInsights.DA.Repository.Interface;
using AdInsights.DA.Repository;
using AdInsights.ServiceLibrary.Services;
using AdInsights.ServiceLibrary.Strategies;
using Google.Cloud.BigQuery.V2;
using StackExchange.Redis;
using AdInsights.ServiceLibrary.Interface;
using AdInsights.DA.Factory.Interface;
using AdInsights.DA.Factory;
using AdInsights.ServiceLibrary.Strategies.Interface;
using AdInsights.API.Middleware;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using NLog.Web;

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


//Configure API Versioning
//builder.Services.AddApiVersioning(options =>
//{
//    options.AssumeDefaultVersionWhenUnspecified = true;
//options.DefaultApiVersion = new ApiVersion(1, 0);
//options.ReportApiVersions = true;
//options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
//});


// Register Service
builder.Services.AddTransient<IAdInsightsService, AdInsightsService>();
builder.Services.AddSingleton<ITenantContextService, TenantContextService>();

// Register Redis & BigQuery Clients
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));
builder.Services.AddSingleton(_ => BigQueryClient.Create("AdServer"));

// Register Repositories
builder.Services.AddSingleton<IRedisAdInsightsRepository, RedisAdInsightsRepository>();
builder.Services.AddSingleton<IBigQueryAdInsightsRepository, BigQueryAdInsightsRepository>();

// Register Strategies
builder.Services.AddTransient<IAdInsightsStrategy, RedisAdInsightsStrategy>();
builder.Services.AddTransient<IAdInsightsStrategy, BigQueryAdInsightsStrategy>();
builder.Services.AddTransient<ICacheStrategy, RedisCacheStrategy>();


// Register Strategy Factory
builder.Services.AddSingleton<IAdInsightsStrategyfactory, AdinsightsStrategyFactory>();
builder.Services.AddSingleton<IAdInsightsRepositoryFactory, AdInsightsRepositoryFactory>();

// Configuring Swagger/OpenAPI 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<TenantValidationMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Register Tenant Service
builder.Services.AddTransient<IAdInsightsService, AdInsightsService>();
builder.Services.AddSingleton<ITenantContextService, TenantContextService>();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
