using MountainTrip.Data;

namespace Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly MountainTripDbContext data;

        public StatisticsService(MountainTripDbContext data)
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalTrips = data.Trips.Count(t => t.IsPublic);
            var totalUsers = data.Users.Count();
            var totalBookings = data.Bookings.Count();

            return new StatisticsServiceModel 
            {
                TotalTrips = totalTrips,
                TotalUsers = totalUsers,
                TotalBookings = totalBookings
            };
        }
    }
}
