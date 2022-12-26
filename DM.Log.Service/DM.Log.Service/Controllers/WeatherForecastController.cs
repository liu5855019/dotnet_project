
namespace DM.Log.Service.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public WeatherForecastController()
        {
        }

        [HttpGet(Name = "GetWeatherForecast555")]
        public IEnumerable<WeatherForecast> Get1()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}



