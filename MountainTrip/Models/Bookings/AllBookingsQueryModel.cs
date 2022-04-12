using MountainTrip.Services.Bookings;

namespace MountainTrip.Models.Bookings
{
    public class AllBookingsQueryModel
    {
        public string TripName { get; set; }

        public string Time { get; set; }

        public byte PeopleCount { get; set; }

        public int TotalBookings { get; set; }

        public IEnumerable<BookingServiceModel> Bookings { get; set; }
    }
}
