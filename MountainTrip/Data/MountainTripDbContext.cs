using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MountainTrip.Data.Models;

namespace MountainTrip.Data
{
    public class MountainTripDbContext : IdentityDbContext<User>
    {        
        public MountainTripDbContext(DbContextOptions<MountainTripDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trip> Trips { get; init; }

        public DbSet<Mountain> Mountains { get; init; }

        public DbSet<Guide> Guides { get; init; }

        public DbSet<Booking> Bookings { get; init; }

        public DbSet<TripBooking> TripsBookings { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Trip>()
                   .HasOne(m => m.Mountain)
                   .WithMany(t => t.Trips)
                   .HasForeignKey(m => m.MountainId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Trip>()
                   .HasOne(m => m.Guide)
                   .WithMany(g => g.Trips)
                   .HasForeignKey(t => t.GuideId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Guide>()
                   .HasOne<User>()
                   .WithOne()
                   .HasForeignKey<Guide>(g => g.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<TripBooking>(e =>
            {
                e.HasKey(pk => new { pk.TripId, pk.BookingId });
            });

            base.OnModelCreating(builder);
        }
    }
}