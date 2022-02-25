namespace MountainTrip.Models.Trips
{
    public class AddTripFormModel
    {       
        public string Name { get; init; }
        
        public string Description { get; init; }

        public double Length { get; init; }

        public string Duration { get; init; }

        public string Difficulty { get; init; }
    
        public string ImageUrl { get; init; }

        public int MountainId { get; init; }
    }
}
