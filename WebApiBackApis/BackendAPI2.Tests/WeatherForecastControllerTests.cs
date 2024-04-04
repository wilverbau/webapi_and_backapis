using BackendAPI2.Service;
using BackendAPI2.Service.Controllers;
using BackendAPI2.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;


namespace BackendAPI2.Tests
{
    public class WeatherForecastControllerTests
    {
        private WeatherService _weatherService;
        private WeatherForecastController _weatherForecastController;
        private readonly Mock<ILogger<WeatherForecastController>> _mockLogger;

        public WeatherForecastControllerTests()
        {
            _weatherService = new WeatherService();
            _mockLogger = new Mock<ILogger<WeatherForecastController>>();
            _weatherForecastController = new WeatherForecastController(_weatherService, _mockLogger.Object);
        }

        [Fact]
        public void GetAllTest()
        {
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