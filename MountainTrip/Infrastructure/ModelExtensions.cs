using MountainTrip.Services.Trips;

namespace MountainTrip.Infrastructure
{
    public static class ModelExtensions
    {
        public static string ToFriendlyUrl(this LatestTripServiceModel trip)
            => trip.ToFriendlyUrl();
    }
}
