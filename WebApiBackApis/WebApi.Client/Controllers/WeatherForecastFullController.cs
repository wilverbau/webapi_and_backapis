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
            var forecasts = await httpClientAPI2.GetFromJsonAsync<IEnumerable<WeatherForecast>>("weatherforecast"); //.GetAsync.GetAsync<Enumerable<WeatherForecast>>("weatherforecast");
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
            return weatherForecastFulls.OrderBy(f=>f.Zip);
        }

        [HttpGet("{zip}")]
        public async Task<WeatherForecastFull> GetAsync(int zip)
        {
            var httpClientAPI2 = _httpClientFactory.CreateClient("BackendAPI2");
            WeatherForecast weatherForecast;
            HttpResponseMessage reponse = await httpClientAPI2.GetAsync($"weatherforecast/{zip}");
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
            HttpResponseMessage response = await httpClientAPI3.PostAsJsonAsync(
                "zipcode", zipCode);

            WeatherForecast weatherForecast = new()
            {
                Zip = weatherForecastFull.Zip,
                Date = weatherForecastFull.Date,
                Summary = weatherForecastFull.Summary,
                TemperatureC = weatherForecastFull.TemperatureC
            };

            response = await httpClientAPI2.PostAsJsonAsync("weatherforecast", weatherForecast);
            response.EnsureSuccessStatusCode();
            var scheme = HttpContext.Request.Scheme;
            var host = HttpContext.Request.Host;
            var actionUrl = this.Url.Action();
            var uri = $"{scheme}://{host.Value}{actionUrl}/{weatherForecastFull.Zip}";
            return new Uri(uri);
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
                zipCode = await httpClientAPI3.GetFromJsonAsync<ZipCode>($"zipcode/{zip}");
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                zipCode = new ZipCode { City = "unknown", County = "unknown", State = "unknown", Zip = zip };
            }
            return zipCode;
        }
    }
}