using MountainTrip.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MountainTrip.Models.Trips
{
    public class AllTripsQueryModel
    {
        public string Name { get; init; }

        public IEnumerable<string> Names { get; set; }

        [Display(Name = "Search trip by name:")]
        public string Searching { get; init; }

        public TripSorting Sorting { get; init; }

        public IEnumerable<TripListingViewModel> Trips { get; set; }
    }
}
