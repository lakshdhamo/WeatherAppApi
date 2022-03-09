using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherApp.Domain.Dto;
using WeatherApp.Domain.Helper;
using WeatherApp.Domain.Interfaces;

namespace WeatherApp.Domain.ServiceManager.Weather
{
    public class OpenWeatherMapCity : ICity
    {
        private readonly ILogService _logService;

        public OpenWeatherMapCity(ILogService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// Gets the suggested City list based on the search text
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public async Task<List<Cities>> GetCitiesList(string cityName)
        {
            List<Cities> cities = new();

            using (var client = new HttpClient())
            {
                client.UpdateBase();
                string ulr = String.Format("data/2.5/find?q={0}&appid={1}&units=metric", cityName, Constants.ApiKey);
                var response = await client.GetAsync(ulr).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    dynamic dynJson = JsonConvert.DeserializeObject(result) ?? string.Empty;
                    if (dynJson == null)
                        return cities;


                    _logService.LogInfo("Retrieved City JSON data");
                    /// Fetch the city list from JSON
                    JArray cityValues = (JArray)dynJson.SelectToken("list");
                    foreach (JObject city in cityValues)
                    {
                        /// Construct City list
                        cities.Add(new Cities()
                        {
                            Id = Convert.ToInt32((string)city.SelectToken("id")),
                            Name = (string)city.SelectToken("name"),
                            Country = (string)city.SelectToken("sys.country"),
                            Latitude = (decimal)city.SelectToken("coord.lat"),
                            Longitude = (decimal)city.SelectToken("coord.lon")
                        });
                    }
                    _logService.LogInfo("Constructed City JSON data");

                }
            }

            return cities;
        }
    }
}
