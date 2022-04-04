using AutoMapper;
using AutoMapper.QueryableExtensions;
using MountainTrip.Data;
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

        public TripDetailsServiceModel Details(int id)
            => data.Trips
            .Where(t => t.Id == id)
            .ProjectTo<TripDetailsServiceModel>(mapper.ConfigurationProvider)
            .FirstOrDefault();
    }
}
