using MountainTrip.Services.Bookings;

namespace MountainTrip.Models.Bookings
{
    public class BookingQueryModel
    {
        public string TripName { get; set; }

        public string Time { get; set; }

        public byte PeopleCount { get; set; }

        public IEnumerable<BookingServiceModel> Bookings { get; set; }
    }
}
