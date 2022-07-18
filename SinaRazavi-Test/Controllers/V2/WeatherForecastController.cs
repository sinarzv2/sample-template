using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SinaRazavi_Test.Controllers.V2
{
    [ApiVersion("2")]
    public class WeatherForecastController : V1.WeatherForecastController
    {
        

        public override Task<IEnumerable<WeatherForecast>> Get()
        {
            return base.Get();
        }

        public WeatherForecastController(ILogger<V1.WeatherForecastController> logger, UserManager<User> userManager) : base(logger, userManager)
        {
        }
    }
}
