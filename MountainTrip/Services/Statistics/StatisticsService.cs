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
            var totalTrips = data.Trips.Count();
            var totalUsers = data.Users.Count();

            return new StatisticsServiceModel 
            {
                TotalTrips = totalTrips,
                TotalUsers = totalUsers
            };
        }
    }
}
