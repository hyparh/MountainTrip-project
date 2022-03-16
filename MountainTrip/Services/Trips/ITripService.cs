using MountainTrip.Data.Enums;

namespace MountainTrip.Services.Trips
{
    public interface ITripService
    {
        TripQueryServiceModel All(
            string name = null,
            string searching = null,
            TripSorting sorting = TripSorting.TripName, //TODO: Is it good here?
            int currentPage = 1,
            int tripsPerPage = int.MaxValue,
            bool publicOnly = true);

        //IEnumerable<LatestTripServiceModel> Latest();

        TripDetailsServiceModel Details(int id);        

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
                TripFormModel trip,
                bool isPublic);

        IEnumerable<TripServiceModel> ByUser(string userId);

        IEnumerable<string> AllNames();

        IEnumerable<TripMountainServiceModel> AllMountains();

        bool MountainExists(int mountainId);

        bool IsByGuide(int tripId, int guideId);

        void ChangeVisibility(int tripId);
    }
}
