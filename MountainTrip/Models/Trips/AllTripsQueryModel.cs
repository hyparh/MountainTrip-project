using MountainTrip.Data.Enums;
using MountainTrip.Services.Trips;
using System.ComponentModel.DataAnnotations;

namespace MountainTrip.Services.Trips
{
    public class AllTripsQueryModel
    {
        public const int TripsPerPage = 3;

        public string Name { get; init; }        

        [Display(Name = "Search trip by name:")]
        public string Searching { get; init; }

        public TripSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalTrips { get; set; }

        public IEnumerable<string> Names { get; set; }

        public IEnumerable<TripServiceModel> Trips { get; set; }
    }
}
