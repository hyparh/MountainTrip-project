using MountainTrip.Data;
using MountainTrip.Services.Bookings;
using System.ComponentModel.DataAnnotations;

namespace MountainTrip.Models.Bookings
{
    using static DataConstants.Booking;

    public class BookingFormModel
    {
        [Required]
        [StringLength(TimeMaxLength)]
        [Display(Name = "Time (hh:mm)")]
        public string Time { get; set; }

        [Range(1, 15, ErrorMessage = "People count must be between 1 and 15")]
        [Display(Name = "People Count")]
        public byte PeopleCount { get; set; }

        public int TripId { get; set; }

        [Required]
        [Display(Name = "Day of Week")]
        public string DayOfWeek { get; init; }
    }
}
