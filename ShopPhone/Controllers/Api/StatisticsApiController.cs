namespace ShopPhone.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using ShopPhone.Services.Statistics;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService statistics)
        {
            this.statistics = statistics;
        }

        [HttpGet]

        public StatisticsServiceModel GetStatistics()
        {
            return this.statistics.Total();
        }
    }
}
