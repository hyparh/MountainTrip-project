using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MountainTrip.Data;
using MountainTrip.Services.Home;
using MountainTrip.Services.Trips;
using Services.Statistics;

namespace MountainTrip.Controllers
{
    public class HomeController : Controller
    {
        private readonly MountainTripDbContext data;       
        private readonly IStatisticsService statistics;
        private readonly IMemoryCache cache;
        private readonly IMapper mapper;

        public HomeController(
            MountainTripDbContext data,
            IStatisticsService statistics,
            IMemoryCache cache,
            IMapper mapper)
        {
            this.data = data;
            this.statistics = statistics;
            this.mapper = mapper;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            const string latestTripsCacheKey = "LatestTripsCacheKey";

            var latestTrips = cache.Get<List<LatestTripServiceModel>>(latestTripsCacheKey);

            if (latestTrips is null)
            {
                latestTrips = data.Trips
                .OrderByDescending(t => t.Id)
                .ProjectTo<LatestTripServiceModel>(mapper.ConfigurationProvider)
                .Take(3)
                .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                cache.Set(latestTripsCacheKey, latestTrips, cacheOptions);
            }

            var totalTrips = data.Trips.Count();
            var totalUsers = data.Users.Count();
            var totalBookings = data.Bookings.Count();           

            var totalStatistics = statistics.Total();            

            return View(new IndexViewModel 
            {
                TotalTrips = totalStatistics.TotalTrips,
                TotalUsers = totalStatistics.TotalUsers,
                TotalBookings = totalStatistics.TotalBookings,
                Trips = latestTrips
            });
        }        

        public IActionResult Error() => View();
    }
}