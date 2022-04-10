using AutoMapper;
using MountainTrip.Controllers;
using Moq;
using NUnit.Framework;
using FakeItEasy;
using MountainTrip.Services.Trips;
using MountainTrip.Test.Mocks;
using global::Services.Statistics;
using MountainTrip.Services.Home;
using System.Linq;
using FluentAssertions;
using MountainTrip.Data.Models;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using MountainTrip.Services.Guides;

namespace MountainTrip.Test.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        // Integration test(doesn't fake things, but uses them as they are)

        [Test]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            var fakeStatisticsService = A.Fake<IStatisticsService>();

            A
                .CallTo(() => fakeStatisticsService.Total())
                .Returns(new StatisticsServiceModel());            
        }

        [Test]
        public void ErrorShouldReturnView()
        {
            // Arrange
            HomeController homeController = new HomeController(null, null, null, null);

            // Act
            IActionResult result = homeController.Error();

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }           
    }
}
