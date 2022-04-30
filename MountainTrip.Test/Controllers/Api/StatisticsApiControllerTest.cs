using MountainTrip.Controllers.Api;
using MountainTrip.Test.Mocks;
using NUnit.Framework;

namespace MountainTrip.Test.Controllers.Api
{
    public class StatisticsApiControllerTest
    {
        [Test]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            // Arrange
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            // Act
            var result = statisticsController.GetStatistics();

            // Assert
            Assert.NotNull(result);
            Assert.That(3, Is.EqualTo(result.TotalTrips));
            Assert.That(4, Is.EqualTo(result.TotalUsers));
            Assert.That(5, Is.EqualTo(result.TotalBookings));
        }
    }
}
