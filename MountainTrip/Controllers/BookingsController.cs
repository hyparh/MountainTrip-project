using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MountainTrip.Data;
using MountainTrip.Data.Models;
using MountainTrip.Models.Bookings;

namespace MountainTrip.Controllers
{
    public class BookingsController : Controller
    {
        private readonly MountainTripDbContext data;
        
        public BookingsController(MountainTripDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult AddBooking()
        {
            return View(new BookingFormModel
            {
                Time = "03:23",
                PeopleCount = 7
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddBooking(BookingFormModel booking)
        {
            if (!ModelState.IsValid)
            {
                return View(booking);
            }



            var bookingData = new Booking
            {
                Time = booking.Time,
                PeopleCount = booking.PeopleCount
            };

            data.Bookings.Add(bookingData);
            data.SaveChanges();

            return RedirectToAction("All", "Trips");
        }
    }
}
