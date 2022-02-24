using Microsoft.EntityFrameworkCore;
using MountainTrip.Data;
using MountainTrip.Data.Models;

namespace MountainTrip.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<MountainTripDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(MountainTripDbContext data)
        {
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
    }
}
