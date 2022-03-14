using AutoMapper;
using MountainTrip.Infrastructure;

namespace MountainTrip.Test.Mocks
{
    public static class MapperMock
    {
        public static IMapper Instance 
        {
            get
            {
                var mapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                });

                return new Mapper(mapperConfiguration);
            }
        }
    }
}
