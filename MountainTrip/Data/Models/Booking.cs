using System.ComponentModel.DataAnnotations;

namespace MountainTrip.Data.Models
{
    using static DataConstants.Booking;

    public class Booking
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(TimeMaxLength)]
        public string DateTime { get; set; }

        public IEnumerable<Trip> Trips { get; init; } = new HashSet<Trip>();
    }
}
