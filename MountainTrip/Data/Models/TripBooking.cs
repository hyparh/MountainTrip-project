using System.ComponentModel.DataAnnotations.Schema;

namespace MountainTrip.Data.Models
{
    public class TripBooking
    {
        [ForeignKey(nameof(Booking))]
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        [ForeignKey(nameof(Trip))]
        public int TripId { get; set; }
        public Trip Trip { get; set; }
    }
}
