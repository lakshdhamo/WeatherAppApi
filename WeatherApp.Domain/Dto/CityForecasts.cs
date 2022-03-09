namespace WeatherApp.Domain.Dto
{
    public record CityForecast
    {
        public string ForecastDate { get; init; }
        public string Weather { get; init; }
        public decimal Minimum { get; init; }
        public decimal Maximum { get; init; }
    }
}
