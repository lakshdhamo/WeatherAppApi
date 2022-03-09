using Microsoft.Extensions.Caching.Memory;
using WeatherApp.Domain.Dto;
using WeatherApp.Domain.Interfaces;
using WeatherApp.Domain.ServiceManager.Weather;

namespace WeatherApp.Domain.ServiceManager
{
    public class WeatherManager : IWeatherManager
    {
        private readonly ICacheManager _cacheManager;
        private readonly ILogService _logService;
        public WeatherManager(ICacheManager cacheManager, ILogService logService)
        {
            _cacheManager = cacheManager;
            _logService = logService;
        }

        /// <summary>
        /// Get possible City list
        /// </summary>
        /// <param name="cityName">City name - search string</param>
        /// <returns></returns>
        public async Task<List<Cities>> GetCities(string cityName)
        {
            /// Gets the value from Cache
            _cacheManager.Get<List<Cities>>(cityName.ToLower(), out List<Cities> lstCities);
            if (lstCities != null)
            {
                _logService.LogInfo("City data is available in cache");
                return lstCities;
            }

            /// Used Factory pattern to get the city list from OpenWeatherMap api.
            ICity city = new OpenWeatherMapCity(_logService);
            lstCities = await city.GetCitiesList(cityName).ConfigureAwait(false);
            _logService.LogInfo("Fetched City data from service");

            /// Cache the output
            if (lstCities.Count > 0)
            {
                // Set cache options
                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(24));

                // Set object in cache
                _cacheManager.Add(cityName.ToLower(), lstCities, cacheOptions);
                _logService.LogInfo("City data is added to cache");
            }

            return lstCities;

        }

        /// <summary>
        /// Gets weather report..
        /// </summary>
        /// <param name="latitude">latitude value</param>
        /// <param name="longitude">longtitude value</param>
        /// <returns>Returns weather reports based on latitude & longtitude</returns>
        public async Task<CityWeather> GetWeatherReport(decimal latitude, decimal longitude)
        {
            /// Used Builder pattern to get the Weather data from OpenWeatherMap api.
            WeatherCreator weatherCreator = new(new OpenWeatherMapBuilder(_logService));
            await weatherCreator.CreateWeather(latitude, longitude).ConfigureAwait(false);
            return weatherCreator.GetWeather();

        }


    }
}
