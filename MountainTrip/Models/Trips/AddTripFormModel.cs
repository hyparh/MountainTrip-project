using MountainTrip.Data;
using System.ComponentModel.DataAnnotations;

namespace MountainTrip.Models.Trips
{
    using static DataConstants;

    public class AddTripFormModel
    {
        [Required]
        //here we can write custom error message like: ErrorMessage = "Maximum: {0}, {1} or {3}..."
        [StringLength(TripNameMaxLength, MinimumLength = TripNameMinLength)]
        public string Name { get; init; }

        [Required]        
        [StringLength(int.MaxValue, 
            MinimumLength = TripDescriptionMinLength,            
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; init; }

        public double Length { get; init; }

        [Required]
        public string Duration { get; init; }

        [Required]
        public string Difficulty { get; init; }

        [Required]
        [Display(Name = "Image URL")]
        [Url]
        public string ImageUrl { get; init; }

        [Required]
        [Display(Name = "Mountain")]
        public int MountainId { get; init; }

        //mountains which we want to visualize in the view
        public IEnumerable<TripMountainViewModel> Mountains { get; set; }
    }
}
