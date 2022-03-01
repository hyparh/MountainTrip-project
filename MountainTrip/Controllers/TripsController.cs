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
            Mountains = GetTripMountains()
        });

        public IActionResult All(string searching)
        {
            var tripsQuery = data.Trips.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searching))
            {
                tripsQuery = tripsQuery.Where(t => 
                    t.Name.Contains(searching, StringComparison.OrdinalIgnoreCase) || 
                    t.Duration.Contains(searching, StringComparison.OrdinalIgnoreCase) || 
                    t.Description.Contains(searching, StringComparison.OrdinalIgnoreCase));
            }

            var trips = tripsQuery
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
                .ToList();

            return View(new AllTripsQueryModel 
            {
                Trips = trips,
                Searching = searching
            });
        }

        // model binding

        [HttpPost]
        public IActionResult Add(AddTripFormModel trip)
        {
            if (!data.Mountains.Any(m => m.Id == trip.MountainId))
            {
                ModelState.AddModelError(nameof(trip.MountainId), "Mountain does not exist.");
            }

            //ModelState.IsValid indicates if it was possible to bind the incoming values from the request to the
            //model correctly and whether any explicitly specified validation rules were broken during the model
            //binding process.

            if (!ModelState.IsValid)
            {
                trip.Mountains = GetTripMountains();

                return View(trip);
            }

            bool IsDifficultyValid = Enum.TryParse(typeof(DifficultyTypes), trip.Difficulty, out object difficulty);

            if (!IsDifficultyValid) //this is not necessary
            {
                throw new ArgumentException("Difficulty is not valid.");
            }

            var tripData = new Trip 
            {                
                Name = trip.Name,
                Description = trip.Description,
                Length = trip.Length,
                Difficulty = (DifficultyTypes)difficulty,
                Duration = trip.Duration,
                ImageUrl = trip.ImageUrl,
                MountainId = trip.MountainId
            };

            data.Trips.Add(tripData);
            data.SaveChanges();

            return RedirectToAction(nameof(All));
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
