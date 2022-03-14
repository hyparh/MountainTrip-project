using MountainTrip.Controllers.Api;
using MountainTrip.Test.Mocks;
using Xunit;

namespace MountainTrip.Test.Controllers.Api
{
    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatisticks()
        {
            // Arrange
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            // Act
            var result = statisticsController.GetStatistics();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.TotalTrips);
            Assert.Equal(4, result.TotalUsers);
            Assert.Equal(5, result.TotalBookings);
        }
    }
}
