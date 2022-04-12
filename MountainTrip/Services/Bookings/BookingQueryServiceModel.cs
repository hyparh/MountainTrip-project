namespace MountainTrip.Services.Bookings
{
    public class BookingQueryServiceModel
    {
        public int TotalBookings { get; init; }

        public IEnumerable<BookingServiceModel> Bookings { get; init; }
    }
}
