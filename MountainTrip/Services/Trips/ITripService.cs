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

        TripDetailsServiceModel Details(int id);

        // TODO: next ones must be done to follow conventions

        int Create(string name,
                string description,
                double length,
                string difficulty,
                string duration,
                string imageUrl,
                int mountainId,
                int guideId,
                TripFormModel trip);

        bool Edit(int tripId,
                string name,
                string description,
                double length,
                string difficulty,
                string duration,
                string imageUrl,
                int mountainId,
                TripFormModel trip);

        IEnumerable<TripServiceModel> ByUser(string userId);

        IEnumerable<string> AllNames();

        IEnumerable<TripMountainServiceModel> AllMountains();

        bool MountainExists(int mountainId);

        bool IsByGuide(int tripId, int guideId);
    }
}
