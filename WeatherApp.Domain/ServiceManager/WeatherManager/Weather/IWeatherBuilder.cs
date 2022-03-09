using WeatherApp.Domain.Dto;

namespace WeatherApp.Domain.ServiceManager.Weather
{
    internal interface IWeatherBuilder
    {
        /// <summary>
        /// Get the JSON data (Weather data) from OpenWeatherMap api.
        /// Build Step1
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        Task GetDataFromExternalServer(decimal latitude, decimal longitude);

        /// <summary>
        /// Extract the Current weather data from JSON
        /// Build Step2
        /// </summary>
        void ExtractCurrentWeather();

        /// <summary>
        /// Extract forecast data from JSON
        /// Build Step3
        /// </summary>
        /// Fetch Forecast data
        void ExtractForecastWeather();

        /// <summary>
        /// Extract Hourly weather data
        /// Build Step4
        /// </summary>
        void ExtractHourlyWeather();

        /// <summary>
        /// Gets the Weather data
        /// </summary>
        /// <returns>Weather data</returns>
        /// <exception cref="InvalidOperationException"></exception>
        CityWeather GetWeather();

    }
}
