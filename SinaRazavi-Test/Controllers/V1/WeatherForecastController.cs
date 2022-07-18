using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Constant;
using Common.Utilities;
using Domain.Entities.IdentityModel;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using SinaRazavi_Test.Filters;

namespace SinaRazavi_Test.Controllers.V1
{
    [ApiVersion("1")]
    public class WeatherForecastController : BaseV1Controller
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly UserManager<User> _userManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        [CustomAuthorize("WeatherForecast.Get")]
        public virtual async Task<IEnumerable<WeatherForecast>> Get()
        {
            var d = User.Identity as ClaimsIdentity;
            var f = d.Claims.ToList();

            var user =  await _userManager.FindByIdAsync(User.Identity.GetUserId());
            var roles = await _userManager.GetRolesAsync(user);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
