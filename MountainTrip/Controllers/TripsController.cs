using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MountainTrip.Data;
using MountainTrip.Data.Models;
using MountainTrip.Infrastructure;
using MountainTrip.Models.Trips;
using MountainTrip.Services.Trips;

namespace MountainTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripService trips;       
        private readonly MountainTripDbContext data;

        public TripsController(ITripService trips, MountainTripDbContext data)
        {
            this.data = data;
            this.trips = trips;
        }

        public IActionResult All([FromQuery] AllTripsQueryModel query)
        {
            var queryResult = trips.All(
                query.Name,
                query.Searching,
                query.Sorting,
                query.CurrentPage,
                AllTripsQueryModel.TripsPerPage);

            var tripNames = trips.AllTripNames();

            query.TotalTrips = queryResult.TotalTrips;
            query.Names = tripNames;
            query.Trips = queryResult.Trips;

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!UserIsGuide())
            {               
                return RedirectToAction(nameof(GuidesController.Create), "Guides");
            }          

            return View(new AddTripFormModel
            {
                Mountains = GetTripMountains()
            });
        }
        
        // model binding

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddTripFormModel trip)
        {
            var guideId = data.Guides
                .Where(g => g.UserId == User.GetId())
                .Select(g => g.Id)
                .FirstOrDefault();

            if (guideId == 0)
            {
                return RedirectToAction(nameof(GuidesController.Create), "Guides");
            }

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
                MountainId = trip.MountainId,
                GuideId = guideId
            };

            data.Trips.Add(tripData);
            data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsGuide()
            => data.Guides.Any(g => g.UserId == User.GetId());

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
