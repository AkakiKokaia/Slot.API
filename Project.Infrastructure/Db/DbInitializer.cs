using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Project.Domain.Aggregates.Users.Entity;

namespace Project.Infrastructure.Db;

public class DBInitializer
{
    public static async Task InitializeDatabase(IServiceProvider serviceProvider, ProjectDbContext context)
    {

        #region Migrations
        using IServiceScope serviceScope = serviceProvider.CreateScope();

        try
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            var initializer = new DBInitializer();
            await initializer.Seed(context, serviceScope);
        }
        catch (Exception ex)
        {
            var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<ProjectDbContext>>();



            logger.LogError(ex, "An error occurred while migrating or seeding the database.");



            throw;
        }
        #endregion
    }
    #region Seeding
    private async Task Seed(ProjectDbContext context, IServiceScope serviceScope)
    {
        context.Database.EnsureCreated();

        await SeedRoles(context, serviceScope);
        await SeedUsers(context, serviceScope);
    }

    private static async Task SeedRoles(ProjectDbContext context, IServiceScope serviceScope)
    {
        if (!context.Roles.Any())
        {
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            await roleManager.CreateAsync(new Role("User"));

            await roleManager.CreateAsync(new Role("Admin"));
        }
    }

    private static async Task SeedUsers(ProjectDbContext context, IServiceScope serviceScope)
    {
        if (!context.Users.Any())
        {
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var user = new User(
                        "Administrator",
                        "+995598361520",
                        "akakikokaiaa3@gmail.com",
                        "Akaki",
                        "Kokaia");
            await userManager.CreateAsync(user, "Test1234*");

            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
    #endregion
}
