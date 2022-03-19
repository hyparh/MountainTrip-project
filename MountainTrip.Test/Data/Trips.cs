using MountainTrip.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace MountainTrip.Test.Data
{
    public static class Trips
    {
        public static IEnumerable<Trip> TenPublicTrips()
            => Enumerable
                .Range(0, 10)
                .Select(t => new Trip
                {
                    Description = "Some description",
                    Duration = "03h:30m",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a7/Cherni_Vrah_Vitosha_07.jpg/1280px-Cherni_Vrah_Vitosha_07.jpg",
                    Name = "Black Peak",
                    IsPublic = true
                });
    }
}
