using MountainTrip.Services.Trips;

namespace MountainTrip.Services.Home
{
    public class IndexViewModel
    {
        public int TotalTrips { get; init; }

        public int TotalUsers { get; init; }

        public int TotalBookings { get; init; }

        public List<LatestTripServiceModel> Trips { get; init; }
    }
}
