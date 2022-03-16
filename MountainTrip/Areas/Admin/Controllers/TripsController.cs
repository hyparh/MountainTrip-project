using Microsoft.AspNetCore.Mvc;
using MountainTrip.Services.Trips;

namespace MountainTrip.Areas.Admin.Controllers
{
    public class TripsController : AdminController
    {
        private readonly ITripService trips;

        public TripsController(ITripService trips) => this.trips = trips;

        public IActionResult All()
        {
            var trips = this.trips
                .All(publicOnly: false)
                .Trips;

            return View(trips);
        }

        public IActionResult ChangeVisibility(int id)
        {
            trips.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
