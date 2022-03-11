using MountainTrip.Data;
using MountainTrip.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MountainTrip.Areas.Admin;

namespace MountainTrip.Infrastructure
{
    using static AdminConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedMountains(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<MountainTripDbContext>();

            data.Database.Migrate();
        }

        private static void SeedMountains(IServiceProvider services)
        {
            var data = services.GetRequiredService<MountainTripDbContext>();

            if (data.Mountains.Any())
            {
                return;
            }

            data.Mountains.AddRange(new[]
            {
                new Mountain { Name = "Vitosha"},
                new Mountain { Name = "Stara planina"},
                new Mountain { Name = "Rila"},
                new Mountain { Name = "Pirin"},
                new Mountain { Name = "Rodopi"},
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () => 
            {
                if (await roleManager.RoleExistsAsync(AdminRoleName))
                {
                    return;
                }

                var role = new IdentityRole { Name = AdminRoleName };

                await roleManager.CreateAsync(role);

                const string adminEmail = "admin@mountaintrip.com";
                const string adminPassword = "admin123";

                var user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Admin"
                };

                await userManager.CreateAsync(user, adminPassword);

                await userManager.AddToRoleAsync(user, role.Name);
            })
            .GetAwaiter()
            .GetResult();
        }
    }
}
