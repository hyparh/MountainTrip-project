namespace MountainTrip.Services.Trips
{
    public class TripServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public double Length { get; init; }

        public string Duration { get; init; }

        public string Difficulty { get; init; }

        public string ImageUrl { get; init; }
    }
}
