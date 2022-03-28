using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using MountainTrip.Data;
using MountainTrip.Services.Home;
using MountainTrip.Services.Trips;
using Services.Statistics;

namespace MountainTrip.Controllers
{
    public class HomeController : Controller
    {
        private readonly MountainTripDbContext data;
        private readonly IStatisticsService statistics;
        private readonly IMapper mapper;

        public HomeController(
            MountainTripDbContext data,
            IStatisticsService statistics,            
            IMapper mapper)
        {
            this.data = data;
            this.statistics = statistics;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var totalTrips = data.Trips.Count();
            var totalUsers = data.Users.Count();

            var trips = data.Trips
                .OrderByDescending(t => t.Id)
                .ProjectTo<LatestTripServiceModel>(mapper.ConfigurationProvider)
                .Take(3)
                .ToList();

            var totalStatistics = statistics.Total();            

            return View(new IndexViewModel 
            {
                TotalTrips = totalStatistics.TotalTrips,
                TotalUsers = totalStatistics.TotalUsers,
                Trips = trips
            });
        }        

        public IActionResult Error() => View();
    }
}