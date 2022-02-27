using Microsoft.AspNetCore.Mvc;
using MountainTrip.Data;
using MountainTrip.Data.Models;
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
            if (!data.Mountains.Any(m => m.Id == trip.MountainId))
            {
                ModelState.AddModelError(nameof(trip.MountainId), "Mointain does not exist.");
            }

            //ModelState.IsValid indicates if it was possible to bind the incoming values from the request to the
            //model correctly and whether any explicitly specified validation rules were broken during the model
            //binding process.

            if (!ModelState.IsValid)
            {
                trip.Mountains = this.GetTripMountains();

                return View(trip);
            }
            
            var tripData = new Trip 
            {
                Name = trip.Name,
                Description = trip.Description,
                Length = trip.Length,
                Difficulty = trip.Difficulty,
                Duration = trip.Duration
            };

            return RedirectToAction("Index", "Home");
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
