using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MountainTrip.Data;
using MountainTrip.Data.Models;
using MountainTrip.Infrastructure;
using MountainTrip.Models.Bookings;
using System.Globalization;

namespace MountainTrip.Controllers
{
    public class BookingsController : Controller
    {
        // TODO this one doesn't work

        private readonly MountainTripDbContext data;
        
        public BookingsController(MountainTripDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult AddBooking()
        {
            var booking = new BookingFormModel { };

            return View(booking = new BookingFormModel
            {
                Time = booking.Time,
                PeopleCount = booking.PeopleCount
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

            bool isTimeValid = DateTime.TryParseExact(booking.Time, "HH:mm",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime time);

            var bookingData = new Booking
            {
                Time = time.ToString("HH:mm"),
                PeopleCount = booking.PeopleCount,
                UserId = User.Id()
            };

            data.Bookings.Add(bookingData);
            data.SaveChanges();

            return RedirectToAction("All", "Trips");
        }
    }
}
