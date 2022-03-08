using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MountainTrip.Data;
using MountainTrip.Data.Models;
using MountainTrip.Infrastructure;
using MountainTrip.Services.Trips;
using MountainTrip.Services.Guides;

namespace MountainTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripService trips;
        private readonly IGuideService guides;
        private readonly MountainTripDbContext data;

        public TripsController(ITripService trips, MountainTripDbContext data, IGuideService guides)
        {
            this.data = data;
            this.trips = trips;
            this.guides = guides;
        }

        public IActionResult All([FromQuery] AllTripsQueryModel query)
        {
            var queryResult = trips.All(
                query.Name,
                query.Searching,
                query.Sorting,
                query.CurrentPage,
                AllTripsQueryModel.TripsPerPage);

            var tripNames = trips.AllNames();

            query.TotalTrips = queryResult.TotalTrips;
            query.Names = tripNames;
            query.Trips = queryResult.Trips;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myTrips = trips.ByUser(User.GetId());

            return View(myTrips);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!guides.IsGuide(User.GetId()))
            {               
                return RedirectToAction(nameof(GuidesController.Create), "Guides");
            }

            return View(new TripFormModel
            {
                Mountains = trips.AllMountains()
            });
        }
        
        // model binding

        [HttpPost]
        [Authorize]        
        public IActionResult Add(TripFormModel trip)
        {
            var guideId = guides.GetIdByUser(User.GetId());

            if (guideId == 0)
            {
                return RedirectToAction(nameof(GuidesController.Create), "Guides");
            }

            if (!trips.MountainExists(trip.MountainId))
            {
                ModelState.AddModelError(nameof(trip.MountainId), "Mountain does not exist.");
            }

            //ModelState.IsValid indicates if it was possible to bind the incoming values from the request to the
            //model correctly and whether any explicitly specified validation rules were broken during the model
            //binding process.

            if (!ModelState.IsValid)
            {
                trip.Mountains = trips.AllMountains();

                return View(trip);
            }

            //bool IsDifficultyValid = Enum.TryParse(typeof(DifficultyTypes), trip.Difficulty, out object difficulty);

            //if (!IsDifficultyValid) //this is not necessary
            //{
            //    throw new ArgumentException("Difficulty is not valid.");
            //}

            trips.Create(
                trip.Name,
                trip.Description,
                trip.Length,
                trip.Difficulty,
                trip.Duration,
                trip.ImageUrl,
                trip.MountainId,
                guideId,
                trip);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = User.GetId();

            if (!guides.IsGuide(userId))
            {
                return RedirectToAction(nameof(GuidesController.Create), "Guides");
            }

            var trip = trips.Details(id);

            if (trip.UserId != userId)
            {
                return Unauthorized();
            }

            return View(new TripFormModel
            {
                Name = trip.Name,
                Description = trip.Description,
                Length = trip.Length,
                Duration = trip.Duration,
                Difficulty = trip.Difficulty,
                ImageUrl = trip.ImageUrl,
                MountainId = trip.MountainId,
                Mountains = trips.AllMountains()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, TripFormModel trip)
        {
            var guideId = guides.GetIdByUser(User.GetId());

            if (guideId == 0)
            {
                return RedirectToAction(nameof(GuidesController.Create), "Guides");
            }

            if (!trips.MountainExists(trip.MountainId))
            {
                ModelState.AddModelError(nameof(trip.MountainId), "Mountain does not exist.");
            }

            if (!ModelState.IsValid)
            {
                trip.Mountains = trips.AllMountains();

                return View(trip);
            }

            //bool IsDifficultyValid = Enum.TryParse(typeof(DifficultyTypes), trip.Difficulty, out object difficulty);

            //if (!IsDifficultyValid)
            //{
            //    throw new ArgumentException("Difficulty is not valid.");
            //}

            if (!trips.IsByGuide(id, guideId))
            {
                return BadRequest();
            }

            trips.Edit(
                id,
                trip.Name,
                trip.Description,
                trip.Length,
                trip.Difficulty,
                trip.Duration,
                trip.ImageUrl,
                trip.MountainId,
                trip);

            return RedirectToAction(nameof(All));
        }
    }
}
