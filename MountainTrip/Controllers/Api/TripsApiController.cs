using Microsoft.AspNetCore.Mvc;
using MountainTrip.Data;
using MountainTrip.Data.Enums;
using MountainTrip.Models.Api.Trips;

namespace MountainTrip.Controllers.Api
{
    [ApiController]
    [Route("api/trips")] // this one is mandatory
    public class TripsApiController : ControllerBase
    {
        private readonly MountainTripDbContext data;

        public TripsApiController(MountainTripDbContext data)
            => this.data = data;

        // here model binding is easier, ModelState is validated automatically

        [HttpGet]
        public ActionResult<AllTripsApiResponseModel> All([FromQuery] AllTripsApiRequestModel query)
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
                .Skip((query.CurrentPage - 1) * query.TripsPerPage)
                .Take(query.TripsPerPage)
                .Select(t => new TripResponseModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Difficulty = t.Difficulty.ToString(),
                    Duration = t.Duration,
                    ImageUrl = t.ImageUrl,
                    Length = t.Length
                })
                .ToList();

            return new AllTripsApiResponseModel
            {
                CurrentPage = query.CurrentPage,
                TripsPerPage = query.TripsPerPage,
                TotalTrips = totalTrips,
                Trips = trips
            };      
        }
    }
}
