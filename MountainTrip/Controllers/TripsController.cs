using Microsoft.AspNetCore.Mvc;
using MountainTrip.Models.Trips;

namespace MountainTrip.Controllers
{
    public class TripsController : Controller
    {
        public IActionResult Add() => View();

        // model binding
        [HttpPost]
        public IActionResult Add(AddTripFormModel trip)
        {


            return View();
        }
    }
}
