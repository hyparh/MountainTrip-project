namespace MountainTrip.Services.Trips
{
    public class TripDetailsServiceModel : TripServiceModel
    {
        public string Description { get; init; }

        public int MountainId { get; init; }

        public int GuideId { get; init; }

        public string GuideFullName { get; init; }
       
        public string UserId { get; init; }
    }
}
