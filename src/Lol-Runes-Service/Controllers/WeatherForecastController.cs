using Lol_Runes_Service.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lol_Runes_Service.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRunaRepository runaRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRunaRepository runaRepository)
        {
            _logger = logger;
            this.runaRepository = runaRepository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetWeatherForecast/Test")]
        public string GetTest()
        {
            return runaRepository.GetInfo();
        }
    }
}