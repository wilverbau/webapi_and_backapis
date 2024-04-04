using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Client.Models;

namespace WebApi.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastFullController : ControllerBase
    {

        private readonly ILogger<WeatherForecastFullController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherForecastFullController(IHttpClientFactory httpClientFactory, ILogger<WeatherForecastFullController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecastFull>> GetAsync()
        {
            var httpClientAPI2 = _httpClientFactory.CreateClient("BackendAPI2");
            var forecasts = await httpClientAPI2.GetFromJsonAsync<IEnumerable<WeatherForecast>>("");
            List<WeatherForecastFull> weatherForecastFulls = new List<WeatherForecastFull>();
            foreach(var forecast in forecasts)
            {
                var zipCode = await this.GetZipCode(forecast.Zip);
                weatherForecastFulls.Add(new()
                {
                    Zip = forecast.Zip,
                    TemperatureC = forecast.TemperatureC,
                    Summary = forecast.Summary,
                    Date = forecast.Date,
                    City = zipCode.City,
                    County = zipCode.County,
                    State = zipCode.State
                }) ;
            }
            return weatherForecastFulls.OrderBy(f=>f.Zip).ThenByDescending(f=>f.Date);
        }

        [HttpGet("{zip}")]
        public async Task<WeatherForecastFull> GetAsync(int zip)
        {
            var httpClientAPI2 = _httpClientFactory.CreateClient("BackendAPI2");
            WeatherForecast weatherForecast;
            HttpResponseMessage reponse = await httpClientAPI2.GetAsync($"{zip}");
            if (reponse.IsSuccessStatusCode)
            {
                weatherForecast = await reponse.Content.ReadFromJsonAsync<WeatherForecast>();

                var zipCode = await this.GetZipCode(zip);
                
                return new WeatherForecastFull
                {
                    Zip = zip,
                    TemperatureC = weatherForecast.TemperatureC,
                    Summary = weatherForecast.Summary,
                    Date = weatherForecast.Date,
                    City = zipCode.City,
                    State = zipCode.State,
                    County = zipCode.County
                };
 
            }
            return null;

        }

        /// <summary>
        /// Adds a new WeatherForecastFull.
        /// If the ZipCode part already existed, then it is not replaced.
        /// If it didn't exist, then it is added.
        /// </summary>
        /// <param name="weatherForecastFull"></param>
        /// <returns>the URI pointing to the newly added element</returns>
        [HttpPost]
        public async Task<Uri> CreateWeatherForecastFullAsync(WeatherForecastFull weatherForecastFull)
        {
            var httpClientAPI2 = _httpClientFactory.CreateClient("BackendAPI2");
            var httpClientAPI3 = _httpClientFactory.CreateClient("BackendAPI3");
            ZipCode zipCode = new() { 
                City = weatherForecastFull.City, 
                County = weatherForecastFull.County , 
                State = weatherForecastFull.State,
                Zip = weatherForecastFull.Zip
            };
            HttpResponseMessage response = await httpClientAPI3.PostAsJsonAsync("", zipCode);

            WeatherForecast weatherForecast = new()
            {
                Zip = weatherForecastFull.Zip,
                Date = weatherForecastFull.Date,
                Summary = weatherForecastFull.Summary,
                TemperatureC = weatherForecastFull.TemperatureC
            };

            response = await httpClientAPI2.PostAsJsonAsync("", weatherForecast);
            if (response.IsSuccessStatusCode)
            {
                var scheme = HttpContext.Request.Scheme;
                var host = HttpContext.Request.Host;
                var actionUrl = this.Url.Action();
                var uri = $"{scheme}://{host.Value}{actionUrl}/{weatherForecastFull.Zip}";
                return new Uri(uri);
            }
            return null;
        }

        /// <summary>
        /// Returns a ZipCode if found in the backend API, otherwise returns a ZipCode
        /// with default values "unknown"
        /// </summary>
        /// <param name="zip"></param>
        /// <returns>a ZipCode instance</returns>
        private async Task<ZipCode> GetZipCode(int zip)
        {
            ZipCode zipCode;
            var httpClientAPI3 = _httpClientFactory.CreateClient("BackendAPI3");
            try
            {
                zipCode = await httpClientAPI3.GetFromJsonAsync<ZipCode>($"{zip}");
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                zipCode = new ZipCode { City = "unknown", County = "unknown", State = "unknown", Zip = zip };
            }
            return zipCode;
        }
    }
}
