namespace WeatherApp.Domain.Dto
{
    public record CityWeather
    {
        private List<CityForecast> _forecasts;
        private List<HourlyWeather> _hourlyWeather;
        public CityWeather()
        {
            _forecasts = new List<CityForecast>();
            _hourlyWeather = new List<HourlyWeather>();
        }

        public string CurrentDate { get; init; }
        public decimal Temperature { get; set; }
        public string Weather { get; set; }
        public decimal WindSpeed { get; set; }
        public decimal WindDegree { get; set; }
        public Int16 Humidity { get; set; }
        public Int16 Visibility { get; set; }
        public Int16 Pressure { get; set; }
        public List<CityForecast> Forecasts { get { return _forecasts; } }
        public List<HourlyWeather> HourlyWeather { get { return _hourlyWeather; } }

    }
}
