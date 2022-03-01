using MountainTrip.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MountainTrip.Models.Trips
{
    public class AllTripsQueryModel
    {
        public IEnumerable<string> Names { get; init; }

        [Display(Name = "Search")]
        public string Searching { get; init; }

        public TripSorting Sorting { get; init; }

        public IEnumerable<TripListingViewModel> Trips { get; init; }
    }
}
