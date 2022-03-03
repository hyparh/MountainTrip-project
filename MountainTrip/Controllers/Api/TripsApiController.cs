using Microsoft.AspNetCore.Mvc;
using MountainTrip.Data;
using System.Collections;

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
        public IEnumerable GetTrip()
        {
            //here we don't tell "return View()" but instead:
            return data.Trips.ToList();
        }        
    }
}
