using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MountainTrip.Data.Models
{
    using static DataConstants.Trip;

    public class Trip
    {        
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public double Length { get; set; }

        [Required]
        public string Duration { get; set; }

        public bool IsPublic { get; set; }

        public bool IsDeleted { get; set; }
       
        public DifficultyTypes Difficulty { get; set; }

        [Required]       
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(Mountain))]
        public int MountainId { get; set; }
        public Mountain Mountain { get; set; }

        [ForeignKey(nameof(Guide))]
        public int GuideId { get; set; }
        public Guide Guide { get; set; }

        [ForeignKey(nameof(Booking))]
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}
