namespace MountainTrip.Views.Trips
{
    public class TripListingViewModel
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public double Length { get; set; }
        
        public string Duration { get; set; }

        public string Difficulty { get; set; }

        public string ImageUrl { get; set; }
    }
}
