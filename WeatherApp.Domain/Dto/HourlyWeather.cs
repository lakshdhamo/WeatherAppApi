namespace WeatherApp.Domain.Dto
{
    public record HourlyWeather
    {
        public string Hour { get; init; }
        public decimal Temperature { get; init; }
    }
}
