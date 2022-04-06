using AutoMapper;
using AutoMapper.QueryableExtensions;
using MountainTrip.Data;
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

        public int UserId(string userId)
            => data.Bookings
                .Where(g => g.UserId == userId)
                .Select(g => g.Id)
                .FirstOrDefault();

        public BookingServiceModel MyBookings(string time, byte peopleCount, string userId)
        {
            return new BookingServiceModel
            {
                Time = time,
                PeopleCount = peopleCount,
                UserId = userId
            };
        }
    }
}
