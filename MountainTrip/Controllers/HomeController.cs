using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MountainTrip.Data;
using MountainTrip.Services;
using MountainTrip.Services.Home;
using Services.Statistics;

namespace MountainTrip.Controllers
{
    public class HomeController : Controller
    {
        private readonly MountainTripDbContext data;
        private readonly IStatisticsService statistics;

        public HomeController(
            IStatisticsService statistics,
            MountainTripDbContext data)
        {
            this.data = data;
            this.statistics = statistics;
        }

        public IActionResult Index()
        {
            var totalTrips = data.Trips.Count();
            var totalUsers = data.Users.Count();

            var trips = data.Trips
                .OrderByDescending(t => t.Id)
                .Select(t => new TripIndexViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Difficulty = t.Difficulty.ToString(),
                    Duration = t.Duration,
                    ImageUrl = t.ImageUrl,
                    Length = t.Length
                })
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });        
    }
}