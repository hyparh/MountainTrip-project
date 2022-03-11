using AutoMapper;
using MountainTrip.Data.Models;
using MountainTrip.Services.Home;
using MountainTrip.Services.Trips;

namespace MountainTrip.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TripDetailsServiceModel, TripFormModel>();
            CreateMap<Trip, TripIndexViewModel>();

            CreateMap<Trip, TripDetailsServiceModel>()
                .ForMember(t => t.UserId, cfg => cfg.MapFrom(t => t.Guide.UserId));                
        }
    }
}
