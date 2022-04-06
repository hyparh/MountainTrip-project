using MountainTrip.Models.Bookings;

namespace MountainTrip.Services.Bookings
{
    public interface IBookingService
    {
        // TODO empty service here
        public int UserId(string userId);

        BookingServiceModel MyBookings(string time, byte peopleCount, string userId);
    }
}
