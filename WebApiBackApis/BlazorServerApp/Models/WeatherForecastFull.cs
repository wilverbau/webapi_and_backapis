using System.ComponentModel.DataAnnotations;

namespace BlazorServerApp.Models
{
    public class WeatherForecastFull
    {
        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
        
        [Required]
        public int Zip { get; set; }

        [Required]
        [MinLength(2)]
        public string City { get; set; }

        [Required]
        [MinLength(2)]
        public string County { get; set; }

        [Required]
        [MinLength(2)]
        public string State { get; set; }
    }
}
