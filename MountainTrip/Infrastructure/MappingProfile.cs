using AutoMapper;
using MountainTrip.Data.Models;
using MountainTrip.Services.Bookings;
using MountainTrip.Services.Trips;

namespace MountainTrip.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Mountain, TripMountainServiceModel>();

            CreateMap<Booking, BookingServiceModel>();

            CreateMap<TripDetailsServiceModel, TripFormModel>();
            CreateMap<Trip, LatestTripServiceModel>();

            CreateMap<Trip, TripServiceModel>()
                .ForMember(t => t.MountainName, cfg => cfg.MapFrom(t => t.Mountain.Name));

            CreateMap<Trip, TripDetailsServiceModel>()
                .ForMember(t => t.UserId, cfg => cfg.MapFrom(t => t.Guide.UserId))
                .ForMember(t => t.MountainName, cfg => cfg.MapFrom(t => t.Mountain.Name));
        }
    }
}
