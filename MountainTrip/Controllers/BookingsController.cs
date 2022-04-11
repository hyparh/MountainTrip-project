using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MountainTrip.Data;
using MountainTrip.Data.Models;
using MountainTrip.Infrastructure;
using MountainTrip.Models.Bookings;
using MountainTrip.Services.Trips;
using System.Globalization;
using System.Linq;

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
            var booking = new BookingFormModel { };

            return View(booking = new BookingFormModel
            {
                Time = booking.Time,
                PeopleCount = booking.PeopleCount
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddBooking(BookingFormModel booking, TripDetailsServiceModel tripDetails)
        {
            if (!ModelState.IsValid)
            {
                return View(booking);
            }

            int tripId = tripDetails.Id;

            bool isTimeValid = DateTime.TryParseExact(booking.Time, "HH:mm",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime time);

            bool IsDayOfWeekValid = Enum.TryParse(typeof(DayOfWeek),
                booking.DayOfWeek, out object parsedDayOfWeek);

            Booking bookingData = new Booking
            {
                Time = time.ToString("HH:mm"),
                PeopleCount = booking.PeopleCount,
                UserId = User.Id(),
                TripId = tripId,
                DayOfWeek = (DayOfWeek)parsedDayOfWeek,
            };

            TripBooking mappingTable = new TripBooking
            {
                Booking = bookingData,
                TripId = tripId
            };

            data.Bookings.Add(bookingData);
            data.TripsBookings.Add(mappingTable);
            data.SaveChanges();

            return RedirectToAction("All", "Trips");
        }

        [Authorize]
        public IActionResult MyBookings(BookingFormModel query)
        {
            var trips = data.Bookings.Where(x => x.TripId == query.TripId);

            return View(trips);
        }
    }
}
