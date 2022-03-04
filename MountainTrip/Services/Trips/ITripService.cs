using MountainTrip.Data.Enums;

namespace MountainTrip.Services.Trips
{
    public interface ITripService
    {
        TripQueryServiceModel All(
            string name,
            string searching,
            TripSorting sorting,
            int currentPage,
            int tripsPerPage);

        IEnumerable<string> AllTripNames();
    }
}
