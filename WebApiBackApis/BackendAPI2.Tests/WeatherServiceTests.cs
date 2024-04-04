using BackendAPI2.Service;
using BackendAPI2.Service.Models;

namespace BackendAPI2.Tests
{
    public class WeatherServiceTests
    {
        private readonly WeatherService _weatherService;
        public WeatherServiceTests()
        {
           _weatherService = new WeatherService();
        }
        [Fact]
        public void GetAllTest()
        {
            // Act
            var results = _weatherService.GetAll();
            //Assert
            Assert.True(results.Any());
            Assert.True(results.Count() > 0);
        }

        [Fact]
        public void GetByZipTest()
        {
            // Arrange
            var existingZip = 11111;
            var unexistingZip = 99999;

            // Act
            var result1 = _weatherService.GetByZip(existingZip);
            var result2 = _weatherService.GetByZip(unexistingZip);

            //Assert
            Assert.NotNull(result1);
            Assert.IsType<WeatherForecast>(result1);
            Assert.Equal(existingZip, result1.Zip);
            Assert.Null(result2);
        }

        [Fact]
        public void AddWeatherForecastTest()
        {
            //Arrange
            var today = DateOnly.FromDateTime(DateTime.Now) ;
            var weatherforecast1 = new WeatherForecast() { Zip = 50001, Date = today, Summary = "cool", TemperatureC = 15 };
            var weatherforecast2 = new WeatherForecast() { Zip = 50001, Date = today, Summary = "warm", TemperatureC = 25 };
            var weatherforecast3 = new WeatherForecast() { Zip = 50001, Date = today.AddDays(1), Summary = "cool", TemperatureC = 15 };
            var weatherforecast4 = new WeatherForecast() { Zip = 50002, Date = today, Summary = "warm", TemperatureC = 25 };

            //Act, Assert
            bool add = _weatherService.AddWeatherForecast(weatherforecast1);
            Assert.True(add);

            add = _weatherService.AddWeatherForecast(weatherforecast2);
            Assert.False(add);

            add = _weatherService.AddWeatherForecast(weatherforecast3);
            Assert.True(add);

            add = _weatherService.AddWeatherForecast(weatherforecast4);
            Assert.True(add);

        }
    }
}