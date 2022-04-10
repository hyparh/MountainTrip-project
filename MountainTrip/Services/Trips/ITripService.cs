using MountainTrip.Data.Enums;
using MountainTrip.Models.Bookings;
using MountainTrip.Services.Bookings;

namespace MountainTrip.Services.Trips
{
    public interface ITripService
    {
        TripQueryServiceModel All(
            string name = null,
            string searching = null,
            TripSorting sorting = TripSorting.TripName,
            int currentPage = 1,
            int tripsPerPage = int.MaxValue,
            bool publicOnly = true);

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

        void Delete(int id);

        IEnumerable<TripServiceModel> ByUser(string userId);

        IEnumerable<string> AllNames();

        IEnumerable<TripMountainServiceModel> AllMountains();       

        bool MountainExists(int mountainId);

        bool IsByGuide(int tripId, int guideId);

        void ChangeVisibility(int tripId);       
    }
}
