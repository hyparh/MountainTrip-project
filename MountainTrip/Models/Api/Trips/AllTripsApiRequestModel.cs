using MountainTrip.Data.Enums;

namespace MountainTrip.Services.Api.Trips
{
    public class AllTripsApiRequestModel
    {
        public string Name { get; init; }
       
        public string Searching { get; init; }

        public TripSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TripsPerPage { get; init; } = 10;        
    }
}
