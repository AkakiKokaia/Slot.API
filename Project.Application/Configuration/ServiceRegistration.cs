using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Project.Application.Configuration.Behaviors;
using Project.Application.Services;
using Project.Domain.Aggregates.Users.Interfaces;
using Project.Shared.Interfaces;
using System.Reflection;

namespace Project.Application.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        AddSwaggerConfiguration(services);
        AddFluentValidation(services);
        AddCustomServices(services);

        return services;
    }
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
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
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new string[] {}
            }
            });
        });
    }

    public static void AddFluentValidation(this IServiceCollection services)
    {

        services
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();

        var assembly = Assembly.Load("Project.Application");

        services.AddValidatorsFromAssembly(assembly);
    }

    public static void AddCustomServices(this IServiceCollection services)
    {
        services
            .AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssemblies(
                     AppDomain.CurrentDomain.GetAssemblies()));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<ISlotService, SlotService>();
        services.AddScoped<ISlotLogicService, SlotLogicService>();
    }
}
