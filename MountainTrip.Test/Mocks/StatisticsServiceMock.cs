using Moq;
using Services.Statistics;

namespace MountainTrip.Test.Mocks
{
    public static class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalTrips = 3,
                        TotalUsers = 4,
                        TotalBookings = 5
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}
