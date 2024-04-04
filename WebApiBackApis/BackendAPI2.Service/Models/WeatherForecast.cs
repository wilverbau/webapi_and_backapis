using BackendAPI2.Service.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace BackendAPI2.Service.Models
{
    [ExcludeFromCodeCoverage]
    public class WeatherForecast
    {
        public int Zip { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
