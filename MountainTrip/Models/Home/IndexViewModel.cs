namespace MountainTrip.Models.Home
{
    public class IndexViewModel
    {
        public int TotalTrips { get; init; }

        public int TotalUsers { get; init; }

        public int TotalBookings { get; init; }

        public List<TripIndexViewModel> Trips { get; init; }
    }
}
