namespace BlazorServerApp.Models
{
    public class WeatherForecastFull
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
    }
}
