using MountainTrip.Data;
using System.ComponentModel.DataAnnotations;

namespace MountainTrip.Services.Trips
{
    using static DataConstants.Trip;

    public class AddTripFormModel
    {        
        [Required]        
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; init; }

        [Required]        
        [StringLength(int.MaxValue, 
            MinimumLength = DescriptionMinLength,            
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; init; }

        [Range(0, 2000, ErrorMessage = "Length must be between 0 and 100 km")]
        public double Length { get; init; }

        [Required]
        [RegularExpression(DurationRegex,
            ErrorMessage = "Please enter time in the following format: 00h:00m")]
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
        public IEnumerable<TripMountainServiceModel> Mountains { get; set; }
    }
}
