using AutoMapper;
using MountainTrip.Infrastructure;
using Moq;

namespace MountainTrip.Test.Mocks
{
    public static class MapperMock
    {
        public static IMapper Instance 
        {
            get
            {
                var mapperMock = new Mock<IMapper>();

                mapperMock
                    .SetupGet(m => m.ConfigurationProvider)
                    .Returns(Mock.Of<IConfigurationProvider>());

                return mapperMock.Object;
            }
        }
    }
}
