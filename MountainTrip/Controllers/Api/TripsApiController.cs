using Microsoft.AspNetCore.Mvc;
using MountainTrip.Models.Api.Trips;
using MountainTrip.Services.Trips;

namespace MountainTrip.Controllers.Api
{
    [ApiController]
    [Route("api/trips")] // this one is mandatory
    public class TripsApiController : ControllerBase
    {
        private readonly ITripService trips;

        public TripsApiController(ITripService trips)
            => this.trips = trips;

        // here model binding is easier, ModelState is validated automatically

        [HttpGet]
        public TripQueryServiceModel All([FromQuery] AllTripsApiRequestModel query)
            => trips.All(
                query.Name,
                query.Searching,
                query.Sorting,
                query.CurrentPage,
                query.TripsPerPage);
    }
}
