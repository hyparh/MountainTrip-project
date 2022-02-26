using Microsoft.AspNetCore.Mvc;
using MountainTrip.Data;
using MountainTrip.Models.Trips;

namespace MountainTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly MountainTripDbContext data;

        public TripsController(MountainTripDbContext data) 
            => this.data = data;

        public IActionResult Add() => View(new AddTripFormModel 
        {
            Mountains = this.GetTripMountains()
        });

        // model binding
        [HttpPost]
        public IActionResult Add(AddTripFormModel trip)
        {
            return View();
        }

        private IEnumerable<TripMountainViewModel> GetTripMountains()
            => data.Mountains
               .Select(m => new TripMountainViewModel
               {
                   Id = m.Id,
                   Name = m.Name
               })
               .ToList();
    }
}
