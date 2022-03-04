using MountainTrip.Data;
using Microsoft.AspNetCore.Mvc;
using MountainTrip.Models.Api.Statistics;

namespace MountainTrip.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly MountainTripDbContext data;

        public StatisticsApiController(MountainTripDbContext data)
            => this.data = data;

        [HttpGet]        
        public StatisticsResponseModel GetStatistics()
        {
            var totalTrips = data.Trips.Count();
            var totalUsers = data.Users.Count();

            var statistics = new StatisticsResponseModel
            {
                TotalTrips = totalTrips,
                TotalUsers = totalUsers,
                TotalBookings = 0
            };

            return statistics;
        }
    }
}
