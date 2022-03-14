using MountainTrip.Data;
using MountainTrip.Data.Models;
using MountainTrip.Services.Guides;
using MountainTrip.Test.Mocks;
using Xunit;

namespace MountainTrip.Test.Services
{
    public class GuideServiceTest
    {
        [Fact]
        public void IsGuideShouldReturnTrueWhenIsGuide()
        {
            // Arrange
            var guideService = GetGuideService();

            // Act
            var result = guideService.IsGuide("TestUserId");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsGuideShouldReturnFalseIfUserIsNotGuide()
        {
            // Arrange            
            var guideService = GetGuideService();

            // Act
            var result = guideService.IsGuide("DifferentUserId");

            // Assert
            Assert.False(result);
        }

        private static IGuideService GetGuideService()
        {
            // Common Arrange
            const string userId = "TestUserId";
            const string fullName = "John Snow";
            const string phoneNumber = "+359-888-888";

            var data = DatabaseMock.Instance;

            data.Guides.Add(new Guide
            {
                UserId = userId,
                FullName = fullName,
                PhoneNumber = phoneNumber
            });

            data.SaveChanges();

            return new GuideService(data);
        }
    }
}
