using BackendAPI2.Service.Models;

namespace BackendAPI2.Service
{
    public interface IWeatherService
    {
        /// <summary>
        /// </summary>
        /// <returns>All weather forecasts</returns>
        IEnumerable<WeatherForecast> GetAll();

        /// <summary>
        /// </summary>
        /// <param name="zip">zipcode</param>
        /// <returns>the most recent weather forecast for the given zipcode</returns>
        WeatherForecast GetByZip(int zip);

        /// <summary>
        /// adds a new weatherforecast
        /// </summary>
        /// <param name="weatherForecast"></param>
        /// <returns>true if the addition was successful</returns>
        bool AddWeatherForecast(WeatherForecast weatherForecast);
    }
}
