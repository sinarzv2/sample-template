using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SinaRazavi_Test.Controllers.V2
{
    [ApiVersion("2")]
    public class WeatherForecastController : V1.WeatherForecastController
    {
        public WeatherForecastController(ILogger<V1.WeatherForecastController> logger) : base(logger)
        {
        }

        public override IEnumerable<WeatherForecast> Get()
        {
            return base.Get();
        }
    }
}
