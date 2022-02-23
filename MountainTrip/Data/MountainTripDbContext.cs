using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MountainTrip.Data.Models;

namespace MountainTrip.Data
{
    public class MountainTripDbContext : IdentityDbContext
    {        
        public MountainTripDbContext(DbContextOptions<MountainTripDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trip> Trips { get; init; }

        public DbSet<Mountain> Mountains { get; init; }
    }
}