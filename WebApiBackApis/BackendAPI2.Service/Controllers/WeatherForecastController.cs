using BackendAPI2.Service.Models;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Returns the whole list of WeatherForecast
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherService.GetAll();
        }

        /// <summary>
        /// Returns the most recent weather forecast for the given zipcode.
        /// </summary>
        /// <param name="zipcode"></param>
        /// <returns></returns>
        [HttpGet("{zipcode}")]
        public ActionResult Get(int zipcode)
        {
            var weatherforecast = _weatherService.GetByZip(zipcode);
            if (weatherforecast == null)
                return NotFound();
            return Ok(weatherforecast);
        }

        /// <summary>
        /// Adds the WeatherForecast
        /// </summary>
        /// <param name="weatherForecast"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] WeatherForecast weatherForecast)
        {
            var add = _weatherService.AddWeatherForecast(weatherForecast);
            if (add)
                return Created();
            return BadRequest();

        }
    }
}
