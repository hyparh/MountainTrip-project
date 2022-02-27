using MountainTrip.Data;
using System.ComponentModel.DataAnnotations;

namespace MountainTrip.Models.Trips
{
    using static DataConstants;

    public class AddTripFormModel
    {
        [Required]
        [MinLength(TripNameMinLength)]
        [MaxLength(TripNameMaxLength)]
        public string Name { get; init; }

        [Required]
        [MinLength(TripDescriptionMinLength)]
        public string Description { get; init; }

        public double Length { get; init; }

        public string Duration { get; init; }

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
