using WeatherApp.Domain.Dto;

namespace WeatherApp.Domain.ServiceManager.Weather
{
    internal class WeatherCreator
    {

        readonly IWeatherBuilder _weatherBuilder;

        public WeatherCreator(IWeatherBuilder weatherBuilder)
        {
            _weatherBuilder = weatherBuilder;
        }

        /// <summary>
        /// Specifies the steps to be followed to build Weather data
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public async Task CreateWeather(decimal latitude, decimal longitude)
        {
            await _weatherBuilder.GetDataFromExternalServer(latitude, longitude);
            _weatherBuilder.ExtractCurrentWeather();
            _weatherBuilder.ExtractForecastWeather();
            _weatherBuilder.ExtractHourlyWeather();
        }

        /// <summary>
        /// Gets the Weather data
        /// </summary>
        /// <returns>Weather data</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public CityWeather GetWeather()
        {
            return _weatherBuilder.GetWeather();
        }
    }
}

