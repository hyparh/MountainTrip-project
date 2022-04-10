using MountainTrip.Data;
using MountainTrip.Data.Models;
using MountainTrip.Services.Guides;
using MountainTrip.Test.Mocks;
using NUnit.Framework;
using FakeItEasy;

namespace MountainTrip.Test.Services
{
    [TestFixture]
    public class GuideServiceTest
    {
        [Test]
        public void IsGuideShouldReturnTrueWhenIsGuide()
        {
            // Arrange
            IGuideService guideService = GetGuideService();

            // Act
            bool result = guideService.IsGuide("TestUserId");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsGuideShouldReturnFalseIfUserIsNotGuide()
        {
            // Arrange            
            IGuideService guideService = GetGuideService();

            // Act
            bool result = guideService.IsGuide("DifferentUserId");

            // Assert
            Assert.False(result);
        }

        [Test]
        public void IdByUserShouldReturnIdOfCurrentUser()
        {
            IGuideService guideService = GetGuideService();

            

            //public int IdByUser(string userId)
            //=> data.Guides
            //       .Where(g => g.UserId == userId)
            //       .Select(g => g.Id)
            //       .FirstOrDefault();
        }

        // SetUp method
        private static IGuideService GetGuideService()
        {
            // Common Arrange
            MountainTripDbContext data = DatabaseMock.Instance;

            data.Guides.Add(new Guide
            {
                UserId = "TestUserId",
                FullName = "John Snow",
                PhoneNumber = "+359-888-888"
            });

            data.SaveChanges();

            return new GuideService(data);
        }
    }
}
