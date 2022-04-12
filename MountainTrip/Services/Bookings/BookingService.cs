using AutoMapper;
using AutoMapper.QueryableExtensions;
using MountainTrip.Data;
using MountainTrip.Data.Models;
using MountainTrip.Models.Bookings;
using MountainTrip.Services.Trips;

namespace MountainTrip.Services.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly MountainTripDbContext data;
        private readonly IMapper mapper;

        public BookingService(MountainTripDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public BookingQueryServiceModel AllBookings(
            string time = null,
            string dayOfWeek = null,
            int peopleCount = 0)
        {
            var bookingsQuery = data.Bookings.OrderByDescending(b => b.TripId);

            int totalBookings = bookingsQuery.Count();

            var bookings = GetBookings(bookingsQuery);

            return new BookingQueryServiceModel
            {
                TotalBookings = totalBookings,
                Bookings = bookings
            };
        }

        private IEnumerable<BookingServiceModel> GetBookings(IQueryable<Booking> bookingQuery)
            => bookingQuery
            .ProjectTo<BookingServiceModel>(mapper.ConfigurationProvider)
            .ToList();

        public int UserId(string userId)
            => data.Bookings
                .Where(g => g.UserId == userId)
                .Select(g => g.Id)
                .FirstOrDefault();
    }
}
