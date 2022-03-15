using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MountainTrip.Infrastructure;
using MountainTrip.Services.Trips;
using MountainTrip.Services.Guides;
using AutoMapper;

namespace MountainTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripService trips;
        private readonly IGuideService guides;
        private readonly IMapper mapper;

        public TripsController(ITripService trips, IGuideService guides, IMapper mapper)
        {
            this.trips = trips;
            this.guides = guides;
            this.mapper = mapper;
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
            var myTrips = trips.ByUser(User.Id());

            return View(myTrips);
        }

        public IActionResult Details(int id, string info)
        {
            var trip = trips.Details(id);

            if (!info.Contains(trip.Name))
            {
                return BadRequest(); // TODO check why this does not work
            }

            return View(trip);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!guides.IsGuide(User.Id()))
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
            var guideId = guides.IdByUser(User.Id());

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

            //bool IsDifficultyValid = Enum.TryParse(typeof(DifficultyTypes), trip.Difficulty, out object difficulty);

            //if (!IsDifficultyValid)
            //{
            //    throw new InvalidDataException("Please select difficulty.");
            //}

            if (!ModelState.IsValid)
            {
                trip.Mountains = trips.AllMountains();

                return View(trip);
            }
            
            var tripId = trips.Create(
                trip.Name,
                trip.Description,
                trip.Length,
                trip.Difficulty,
                trip.Duration,
                trip.ImageUrl,
                trip.MountainId,
                guideId,
                trip);

            return RedirectToAction(nameof(Details), new { id = tripId, info = trip.Name + ", " + trip.Duration + ", " + trip.Length });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = User.Id();

            if (!guides.IsGuide(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(GuidesController.Create), "Guides");
            }

            var trip = trips.Details(id);

            if (trip.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var tripForm = mapper.Map<TripFormModel>(trip);
            tripForm.Mountains = trips.AllMountains();

            return View(tripForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, TripFormModel trip)
        {
            var guideId = guides.IdByUser(User.Id());

            if (guideId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(GuidesController.Create), "Guides");
            }

            if (!trips.MountainExists(trip.MountainId))
            {
                ModelState.AddModelError(nameof(trip.MountainId), "Mountain does not exist.");
            }

            //bool IsDifficultyValid = Enum.TryParse(typeof(DifficultyTypes), trip.Difficulty, out object difficulty);

            //if (!IsDifficultyValid)
            //{
            //    throw new InvalidDataException("Please select difficulty.");
            //}

            if (!ModelState.IsValid)
            {
                trip.Mountains = trips.AllMountains();

                return View(trip);
            }
            
            if (!trips.IsByGuide(id, guideId) && !User.IsAdmin())
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

            return RedirectToAction(nameof(Details), new { id, info = trip.Name + ", " + trip.Duration + ", " + trip.Length });
        }
    }
}
