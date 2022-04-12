using MountainTrip.Models.Bookings;

namespace MountainTrip.Services.Bookings
{
    public interface IBookingService
    {
        BookingQueryServiceModel AllBookings(
            string time = null, 
            string dayOfWeek = null,
            int peopleCount = 0);

        public int UserId(string userId);
    }
}
