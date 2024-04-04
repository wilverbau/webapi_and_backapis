using BackendAPI2.Service;
using BackendAPI2.Service.Controllers;
using BackendAPI2.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using static System.Net.WebRequestMethods;

namespace BackendAPI2.Tests
{
    public class WeatherForecastControllerTests
    {
        private readonly HttpClient _httpClient;
        private WeatherService _weatherService;
        private WeatherForecastController _weatherForecastController;
        private readonly Mock<ILogger<WeatherForecastController>> _mockLogger;

        public WeatherForecastControllerTests()
        {
            _weatherService = new WeatherService();
            _mockLogger = new Mock<ILogger<WeatherForecastController>>();
            _weatherForecastController = new WeatherForecastController(_weatherService, _mockLogger.Object);
            var builder = new ConfigurationBuilder().AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "Properties", "launchSettings.json"));
            var Configuration = builder.Build();
            _httpClient =  new HttpClient { BaseAddress = new Uri(Configuration.GetValue<string>("profiles:http:applicationUrl")) };
        }

        [Fact]
        public void GetAllTest()
        {
            //var plutos = await _httpClient.GetAsync("weatherforecast");
            //var forecasts = await _httpClient.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
            //Act
            var results = _weatherForecastController.Get();

            //Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void GetTest()
        {
            //Arrange
            int zipCode = 11111;
            int wrongZipCode = 99999;

            //Act
            var result1 = _weatherForecastController.Get(zipCode);
            var result2 = _weatherForecastController.Get(wrongZipCode);

            //Assert
            Assert.NotNull(result1);
            Assert.IsType<OkObjectResult>(result1);
            var okResult = result1 as OkObjectResult;
            var weatherforecast = okResult.Value as WeatherForecast;
            Assert.Equal(zipCode, weatherforecast.Zip);

            Assert.IsType<NotFoundResult>(result2);
          
        }

        [Fact]
        public void PostTest()
        {
            //Arrange
            WeatherForecast weatherForecast = new() { Zip = 66666, Date = DateOnly.FromDateTime(DateTime.Now), Summary = "hot", TemperatureC = 30 };

            //Act, Assert
            var outcome = _weatherForecastController.Post(weatherForecast);
            Assert.IsType<CreatedResult>(outcome);

            outcome = _weatherForecastController.Post(weatherForecast);
            Assert.IsType<BadRequestResult>(outcome);
        }
    }
}