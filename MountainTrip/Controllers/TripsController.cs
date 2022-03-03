using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MountainTrip.Data;
using MountainTrip.Data.Enums;
using MountainTrip.Data.Models;
using MountainTrip.Infrastructure;
using MountainTrip.Models.Trips;

namespace MountainTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly MountainTripDbContext data;

        public TripsController(MountainTripDbContext data) 
            => this.data = data;

        public IActionResult All([FromQuery] AllTripsQueryModel query)
        {
            var tripsQuery = data.Trips.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                tripsQuery = tripsQuery.Where(t => t.Name == query.Name);
            }

            if (!string.IsNullOrWhiteSpace(query.Searching))
            {
                tripsQuery = tripsQuery.Where(t =>
                    t.Name.ToLower().Contains(query.Searching.ToLower()) ||
                    t.Duration.ToLower().Contains(query.Searching.ToLower()) ||
                    t.Description.ToLower().Contains(query.Searching.ToLower()));
            }

            tripsQuery = query.Sorting switch
            {
                TripSorting.TripDuration => tripsQuery.OrderByDescending(t => t.Duration),
                TripSorting.TripDifficulty => tripsQuery.OrderByDescending(t => t.Difficulty),
                TripSorting.TripName or _ => tripsQuery.OrderByDescending(t => t.Id)
            };

            var totalTrips = tripsQuery.Count();

            var trips = tripsQuery
                .Skip((query.CurrentPage - 1) * AllTripsQueryModel.TripsPerPage)
                .Take(AllTripsQueryModel.TripsPerPage)
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

            var tripNames = data.Trips
                .Select(t => t.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToList();

            query.TotalTrips = totalTrips;
            query.Names = tripNames;
            query.Trips = trips;

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
