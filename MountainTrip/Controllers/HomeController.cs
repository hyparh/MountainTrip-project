using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MountainTrip.Data;
using MountainTrip.Models;
using MountainTrip.Models.Trips;

namespace MountainTrip.Controllers
{
    public class HomeController : Controller
    {
        private readonly MountainTripDbContext data;

        public HomeController(MountainTripDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            var trips = data.Trips
                .OrderByDescending(t => t.Id)
                .Select(t => new TripListingViewModel
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

            return View(trips);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });        
    }
}