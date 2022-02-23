using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MountainTrip.Data.Models
{
    using static DataConstants;

    public class Trip
    {        
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(TripNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public double Length { get; set; }
        
        public TimeSpan Duration { get; set; }

        public Enum Difficulty { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(Mountain))]
        public int MountainId { get; set; }
        public Mountain Mountain { get; init; }
    }
}
