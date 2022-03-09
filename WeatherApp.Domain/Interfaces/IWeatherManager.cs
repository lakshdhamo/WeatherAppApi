using WeatherApp.Domain.Dto;

namespace WeatherApp.Domain.Interfaces
{
    public interface IWeatherManager
    {
        /// <summary>
        /// Get possible City list
        /// </summary>
        /// <param name="cityName">City name - search string</param>
        /// <returns></returns>
        Task<List<Cities>> GetCities(string city);


        /// <summary>
        /// Gets weather report..
        /// </summary>
        /// <param name="latitude">latitude value</param>
        /// <param name="longitude">longtitude value</param>
        /// <returns>Returns weather reports based on latitude & longtitude</returns>
        Task<CityWeather> GetWeatherReport(decimal latitude, decimal longitude);
    }
}
