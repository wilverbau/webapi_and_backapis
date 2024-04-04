using BackendAPI2.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BackendAPI2.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IWeatherService WeatherService, ILogger<WeatherForecastController> logger)
        {
            _weatherService = WeatherService;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            Thread.Sleep(3000);
            return _weatherService.GetAll();
        }

        [HttpGet("{zipcode}")]
        public ActionResult Get(int zipcode)
        {
            Task.Delay(2000).Wait();
            var weatherforecast = _weatherService.GetByZip(zipcode);
            if (weatherforecast == null)
                return NotFound();
            return Ok(weatherforecast);
        }

        [HttpPost]
        public ActionResult Post([FromBody] WeatherForecast weatherForecast)
        {
            var add = _weatherService.AddWeatherForecast(weatherForecast);
            if (add)
            {
                return Created();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
