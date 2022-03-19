namespace MountainTrip.Test.Controllers
{
    using AutoMapper;
    using MountainTrip.Controllers;
    using Moq;
    using Xunit;
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

    using static Data.Trips;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
            => MyMvc
            .Pipeline()
            .ShouldMap("/")
            .To<HomeController>(c => c.Index())
            .Which(controller => controller
                .WithData(TenPublicTrips()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<LatestTripServiceModel>>()
                    .Passing(m => m.Should().HaveCount(3)));

        // Integration test (doesn't fake things, but uses them as they are)

        //[Fact]
        //public void IndexShouldReturnViewWithCorrectModel()
        //{
        //    // Arrange
        //    var data = DatabaseMock.Instance;
        //    var mapper = MapperMock.Instance;

        //    var trips = Enumerable
        //        .Range(0, 10)
        //        .Select(t => new Trip
        //        {
        //            Description = "Some description",
        //            Duration = "03h:30m",
        //            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a7/Cherni_Vrah_Vitosha_07.jpg/1280px-Cherni_Vrah_Vitosha_07.jpg",
        //            Name = "Black Peak"
        //        });

        //    data.Trips.AddRange(trips);
        //    data.Users.Add(new User());
        //    data.SaveChanges();

        //    var tripService = new TripService(data, mapper);
        //    var statisticsService = new StatisticsService(data);

        //    var homeController = new HomeController(data, statisticsService, mapper);

        //    // Act
        //    var result = homeController.Index();

        //    // Assert
        //    Assert.NotNull(result);
        //    var viewResult = Assert.IsType<ViewResult>(result);

        //    var model = viewResult.Model;

        //    var indexViewModel = Assert.IsType<IndexViewModel>(model);

        //    Assert.Equal(3, indexViewModel.Trips.Count);
        //    Assert.Equal(10, indexViewModel.TotalTrips);
        //    Assert.Equal(1, indexViewModel.TotalUsers);
        //}

        //[Fact]
        //public void ErrorShouldReturnView()
        //{
        //    // Arrange
        //    var homeController = new HomeController(null, Mock.Of<IMapper>(), null);
        //
        //    // Act
        //    var result = homeController.Error();
        //
        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<ViewResult>(result);
        //}        

        [Fact]
        public void ErrorShouldReturnView()
            => MyMvc
            .Pipeline()
            .ShouldMap("/Home/Error")
            .To<HomeController>(t => t.Error())
            .Which()
            .ShouldReturn()
            .View();
    }
}
