namespace MountainTrip.Models.Api.Trips
{
    public class AllTripsApiResponseModel
    {
        public int CurrentPage { get; init; }

        public int TripsPerPage { get; init; }

        public int TotalTrips { get; init; }


        public IEnumerable<TripResponseModel> Trips { get; init; }
    }
}
