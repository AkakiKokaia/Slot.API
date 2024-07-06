using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Slot.Domain.Aggregates.Users.Entity;
using Slot.Infrastructure.Db;
using System.Globalization;
using System.Text;

namespace Slot.API;

public static class ServiceRegistration
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services
            .AddHttpContextAccessor()
            .AddEndpointsApiExplorer()
            .AddCors();
        AddLocalizationConfiguration(services);
        AddIdentityConfiguration(services, configuration);
        return services;
    }

    public static void AddLocalizationConfiguration(this IServiceCollection services)
    {
        services.AddLocalization();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
            new CultureInfo("ka-GE"),
            new CultureInfo("en-US")
        };

            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
    }

    public static void AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<SlotDbContext>()
                .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
       .AddJwtBearer(options =>
       {
           options.RequireHttpsMetadata = true;
           options.SaveToken = true;
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = false,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = configuration["JwtSettings:Issuer"],
               IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
               ClockSkew = TimeSpan.FromMinutes(5)
           };
       });

        services.AddAuthorization();
    }
}
