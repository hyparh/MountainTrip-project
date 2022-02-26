using System.ComponentModel.DataAnnotations;

namespace MountainTrip.Models.Trips
{
    public class AddTripFormModel
    {       
        public string Name { get; init; }
        
        public string Description { get; init; }

        public double Length { get; init; }

        public string Duration { get; init; }

        public string Difficulty { get; init; }
    
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Display(Name = "Mountain")]
        public int MountainId { get; init; }

        //mountains which we want to visualize in the view
        public IEnumerable<TripMountainViewModel> Mountains { get; set; }
    }
}
