using MountainTrip.Data;
using System.ComponentModel.DataAnnotations;

namespace MountainTrip.Models.Bookings
{
    using static DataConstants.Booking;

    public class BookingFormModel
    {
        [Required]
        [StringLength(TimeMaxLength)]
        public string Time { get; set; }

        [Required]
        [Range(1, 15, ErrorMessage = "People count must be between 1 and 15")]
        public byte PeopleCount { get; set; }
    }
}
