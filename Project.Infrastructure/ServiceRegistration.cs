using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Project.Domain.Aggregates.Transactions.Interfaces;
using Project.Infrastructure.Db;
using Project.Infrastructure.Repositories;

namespace Project.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProjectDbContext>(
            options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), x =>
                {
                    x.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);
                }).LogTo(Console.WriteLine, LogLevel.Information);
            }, ServiceLifetime.Transient);

        return services;
    }

    public static IServiceCollection AddCustomRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}
