using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MountainTrip.Infrastructure;
using MountainTrip.Services.Trips;
using MountainTrip.Services.Guides;
using AutoMapper;
using MountainTrip.Models.Bookings;
using MountainTrip.Data.Models;
using MountainTrip.Data;
using System.Globalization;
using MountainTrip.Services.Bookings;

namespace MountainTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly MountainTripDbContext data;
        private readonly ITripService trips;
        private readonly IGuideService guides;
        private readonly IMapper mapper;       
        private readonly IBookingService bookings;       

        public TripsController(
            MountainTripDbContext data,
            ITripService trips, 
            IGuideService guides, 
            IMapper mapper,
            IBookingService bookings)
        {
            this.data = data;
            this.trips = trips;
            this.guides = guides;
            this.mapper = mapper;
            this.bookings = bookings;
        }

        public IActionResult All([FromQuery] AllTripsQueryModel query)
        {
            var queryResult = trips.All(
                query.Name,
                query.Searching,
                query.Sorting,
                query.CurrentPage,
                AllTripsQueryModel.TripsPerPage);

            var tripNames = trips.AllNames();

            query.TotalTrips = queryResult.TotalTrips;
            query.Names = tripNames;
            query.Trips = queryResult.Trips;

            return View(query);
        }

        // visualizes each guide's trips

        [Authorize]
        public IActionResult Mine()
        {
            var myTrips = trips.ByUser(User.Id());

            return View(myTrips);
        }

        public IActionResult Details(int id, string info)
        {
            var trip = trips.Details(id);

            if (!info.Contains(trip.Name))
            {
                return BadRequest(); // TODO check why this does not work
            }

            return View(trip);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!guides.IsGuide(User.Id()))
            {               
                return RedirectToAction(nameof(GuidesController.Create), "Guides");
            }

            return View(new TripFormModel
            {
                // Mountains is IEnumerable collection

                Mountains = trips.AllMountains()
            });
        }
        
        // model binding

        [HttpPost]
        [Authorize]        
        public IActionResult Add(TripFormModel trip)
        {
            var guideId = guides.IdByUser(User.Id());

            if (guideId == 0)
            {
                return RedirectToAction(nameof(GuidesController.Create), "Guides");
            }

            if (!trips.MountainExists(trip.MountainId))
            {
                ModelState.AddModelError(nameof(trip.MountainId), "Mountain does not exist.");
            }            

            if (!ModelState.IsValid)
            {
                trip.Mountains = trips.AllMountains();

                return View(trip);
            }
            
            var tripId = trips.Create(
                trip.Name,
                trip.Description,
                trip.Length,
                trip.Difficulty,
                trip.Duration,
                trip.ImageUrl,
                trip.MountainId,
                guideId,
                trip);

            return RedirectToAction(nameof(Details), new 
            { 
                id = tripId, info = trip.Name + ", " + trip.Duration + ", " + trip.Length
            });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = User.Id();

            if (!guides.IsGuide(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(GuidesController.Create), "Guides");
            }

            var trip = trips.Details(id);

            if (trip.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var tripForm = mapper.Map<TripFormModel>(trip);
            tripForm.Mountains = trips.AllMountains();

            return View(tripForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, TripFormModel trip)
        {
            var guideId = guides.IdByUser(User.Id());

            if (guideId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(GuidesController.Create), "Guides");
            }

            if (!trips.MountainExists(trip.MountainId))
            {
                ModelState.AddModelError(nameof(trip.MountainId), "Mountain does not exist.");
            }            

            if (!ModelState.IsValid)
            {
                trip.Mountains = trips.AllMountains();

                return View(trip);
            }
            
            if (!trips.IsByGuide(id, guideId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            var edited = trips.Edit(
                id,
                trip.Name,
                trip.Description,
                trip.Length,
                trip.Difficulty,
                trip.Duration,
                trip.ImageUrl,
                trip.MountainId,
                trip,
                User.IsAdmin());

            if (!edited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Details), new { id, info = trip.Name + ", " + trip.Duration + ", " + trip.Length});
        }

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

        [Authorize]
        public IActionResult Delete(int id)
        {
            var guideId = guides.IdByUser(this.User.Id());

            if (!trips.IsByGuide(id, guideId) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            trips.Delete(id);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult MyBookings(BookingServiceModel query)
        {            
            var bookingsQuery = bookings.MyBookings(query.Time, query.PeopleCount, query.UserId);

            return View(query);
        }
    }
}
