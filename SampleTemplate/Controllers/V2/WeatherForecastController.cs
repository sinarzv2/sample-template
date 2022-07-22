using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SampleTemplate.Controllers.V2
{
    [ApiVersion("2")]
    public class WeatherForecastController : V1.WeatherForecastController
    {

        public WeatherForecastController(ILogger<WeatherForecastController> logger, UserManager<User> userManager) : base(logger, userManager)
        {
        }
        public override Task<IEnumerable<WeatherForecast>> Get()
        {
            return base.Get();
        }

      
    }
}
