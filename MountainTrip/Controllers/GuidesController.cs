using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MountainTrip.Data;
using MountainTrip.Data.Models;
using MountainTrip.Infrastructure;
using MountainTrip.Services.Bookings;
using MountainTrip.Services.Guides;

namespace MountainTrip.Controllers
{    
    public class GuidesController : Controller
    {
        private readonly MountainTripDbContext data;
        private readonly IBookingService booking;


        public GuidesController(MountainTripDbContext data, IBookingService booking)
        {
            this.data = data;
            this.booking = booking;
        }

        [Authorize]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Create(BecomeGuideFormModel guide)
        {
            var userId = User.Id();

            var userIsAlreadyAGuide = data.Guides
                .Any(g => g.UserId == userId);

            if (userIsAlreadyAGuide)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(guide);
            }

            var guideData = new Guide 
            {
                FullName = guide.FullName,
                PhoneNumber = guide.PhoneNumber,
                UserId = userId
            };

            data.Guides.Add(guideData);
            data.SaveChanges();

            return RedirectToAction("All", "Trips");
        }
    }
}
