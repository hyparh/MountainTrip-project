using Moq;
using MountainTrip.Services.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainTrip.Test.Mocks
{
    public class BookingServiceMock
    {
        //public static IBookingService Instance
        //{
        //    get
        //    {
        //        var bookingServiceMock = new Mock<IBookingService>();

        //        string name = null;
        //        string dayOfWeek = null;
        //        int peopleCount = 0;

        //        int totalBookings = 5;

        //        var bookingsQuery = data.Bookings.OrderByDescending(b => b.TripId);

        //        bookingServiceMock
        //            .Setup(b => b.AllBookings(name, dayOfWeek, peopleCount))
        //            .Returns(new BookingQueryServiceModel
        //            {
        //                TotalBookings = totalBookings,
        //                Bookings = 
        //            });

        //        return bookingServiceMock.Object;
        //    }
        //}
    }
}
