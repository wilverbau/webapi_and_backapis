using BackendAPI2.Service.Models;

namespace BackendAPI2.Service
{
    public class WeatherService:IWeatherService
    {
        private static readonly string[] _summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private List<WeatherForecast> _weatherForecasts;

        public WeatherService()
        {
            _weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Zip = int.Parse(string.Format("{0}{0}{0}{0}{0}", index)),
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = _summaries[Random.Shared.Next(_summaries.Length)]
            }).ToList();

        }

        /// <inheritdoc>
        public IEnumerable<WeatherForecast> GetAll()
        {
            //to mimic database latency
            Thread.Sleep(3000);
            return _weatherForecasts;
        }

        /// <inheritdoc>
        public WeatherForecast GetByZip(int zip)
        {
            //to mimic database latency
            Task.Delay(2000).Wait();
            if (!_weatherForecasts.Where(w => w.Zip == zip).Any())
                return null;
            else
                return _weatherForecasts.Where(w => w.Zip == zip).OrderByDescending(w => w.Date).First();
        }

        /// <inheritdoc>
        public bool AddWeatherForecast(WeatherForecast weatherForecast)
        {
            if (_weatherForecasts.Where(w=>w.Zip==weatherForecast.Zip && w.Date==weatherForecast.Date).Any())
            {
                return false;
            }
            _weatherForecasts.Add(weatherForecast);
            return true;
        }

    }
}
