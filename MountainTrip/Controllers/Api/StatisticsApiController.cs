using Microsoft.AspNetCore.Mvc;
using Services.Statistics;

namespace MountainTrip.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService statistics)
            => this.statistics = statistics;

        [HttpGet]        
        public StatisticsServiceModel GetStatistics()
            => statistics.Total();
    }
}
