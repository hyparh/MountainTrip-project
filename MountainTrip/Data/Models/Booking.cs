using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MountainTrip.Data.Models
{
    using static DataConstants.Booking;

    public class Booking
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(TimeMaxLength)]
        public string Time { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public byte PeopleCount { get; set; }

        public ICollection<TripBooking> TripsBookings { get; set; } = new HashSet<TripBooking>();
    }
}
