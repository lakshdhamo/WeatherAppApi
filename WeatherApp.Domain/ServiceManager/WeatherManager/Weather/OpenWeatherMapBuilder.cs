using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherApp.Domain.Dto;
using WeatherApp.Domain.Helper;
using WeatherApp.Domain.Interfaces;

namespace WeatherApp.Domain.ServiceManager.Weather
{
    internal class OpenWeatherMapBuilder : IWeatherBuilder
    {
        private readonly ILogService _logService;
        readonly CityWeather cityWeather;
        dynamic? dynJson = null;
        int weatherSteps = 0;

        public OpenWeatherMapBuilder(ILogService logService)
        {
            _logService = logService;
            cityWeather = new()
            {
                CurrentDate = DateTime.Now.ShortDateStringFormat("yyyy-MM-dd")
            };
        }

        /// <summary>
        /// Get the JSON data (Weather data) from OpenWeatherMap api.
        /// Build Step1
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public async Task GetDataFromExternalServer(decimal latitude, decimal longitude)
        {
            _logService.LogInfo("Weather data fetch Step1 started");
            using var client = new HttpClient();
            client.UpdateBase();
            string ulr = String.Format("data/2.5/onecall?lat={0}&lon={1}&units=metric&appid={2}", latitude, longitude, Constants.ApiKey);
            var response = await client.GetAsync(ulr).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                dynJson = JsonConvert.DeserializeObject(result) ?? string.Empty;

                if (dynJson == null)
                    return;
            }
            IncrementSteps();
        }

        /// <summary>
        /// Extract the Current weather data from JSON
        /// Build Step2
        /// </summary>
        public void ExtractCurrentWeather()
        {
            _logService.LogInfo("Weather data fetch Step2 started");
            cityWeather.Temperature = (decimal)dynJson.SelectToken("current.temp");
            cityWeather.Weather = ExtractWeather(dynJson.SelectToken("current"));
            cityWeather.WindSpeed = (decimal)dynJson.SelectToken("current.wind_speed");
            cityWeather.WindDegree = (decimal)dynJson.SelectToken("current.wind_deg");
            cityWeather.Humidity = (Int16)dynJson.SelectToken("current.humidity");
            cityWeather.Visibility = (Int16)dynJson.SelectToken("current.visibility");
            cityWeather.Pressure = (Int16)dynJson.SelectToken("current.pressure");
            IncrementSteps();
        }

        /// <summary>
        /// Extract forecast data from JSON
        /// Build Step3
        /// </summary>
        public void ExtractForecastWeather()
        {
            _logService.LogInfo("Weather data fetch Step3 started");
            JArray forecasts = (JArray)dynJson.SelectToken("daily");
            int forecastDay = 1;
            foreach (JObject forecast in forecasts)
            {
                if (forecastDay > Constants.ForecastDays)
                    break;
                forecastDay++;

                DateTime forecastDate = ValueConverter.UnixTimeStampToDateTime((double)forecast.SelectToken("dt"));

                cityWeather.Forecasts.Add(
                    new CityForecast()
                    {
                        ForecastDate = forecastDate.ShortDateStringFormat("yyyy-MM-dd"),
                        Weather = ExtractWeather(forecast),
                        Minimum = (decimal)forecast.SelectToken("temp.min"),
                        Maximum = (decimal)forecast.SelectToken("temp.max")
                    }
                    );
            }
            IncrementSteps();
        }

        /// <summary>
        /// Extract Hourly weather data
        /// Build Step4
        /// </summary>
        public void ExtractHourlyWeather()
        {
            _logService.LogInfo("Weather data fetch Step4 started");
            JArray hourly = (JArray)dynJson.SelectToken("hourly");
            int hours = 1;
            foreach (JObject hour in hourly)
            {
                if (hours > Constants.NoOfHours)
                    break;
                hours++;

                DateTime currentDate = ValueConverter.UnixTimeStampToDateTime((double)hour.SelectToken("dt"));
                string hourString = (string)currentDate.ShortDateStringFormat("HH:mm");

                cityWeather.HourlyWeather.Add(
                    new HourlyWeather()
                    {
                        Hour = hourString,
                        Temperature = (decimal)hour.SelectToken("temp")
                    }
                    );
            }
            IncrementSteps();
        }

        /// <summary>
        /// Gets the Weather data
        /// </summary>
        /// <returns>Weather data</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public CityWeather GetWeather()
        {
            /// Prerequisite to complete all 4 previous step. 
            /// If this method is called prior to any of the above 4 steps then this method is invalid operation.
            /// Because CityWeather is premature without the 4 steps
            if (weatherSteps < 4)
            {
                _logService.LogInfo(String.Format("{0} steps were not completed. Due to that, its been completed invalid operation", (4 - weatherSteps).ToString()));
                throw new InvalidOperationException(String.Format("Weather is not created fully. Still {0} step(s) are pending.", 4 - weatherSteps));
            }
            _logService.LogInfo("Weather data created");
            return cityWeather;
        }

        /// <summary>
        /// Maintaining the state of steps completed
        /// </summary>
        private void IncrementSteps()
        {
            ++weatherSteps;
            _logService.LogInfo(String.Format("Weather data fetch Step {0} completed", weatherSteps.ToString()));
        }

        /// <summary>
        /// Extract weather data token from JSON
        /// </summary>
        /// <param name="forecast"></param>
        /// <returns></returns>
        private string ExtractWeather(JObject forecast)
        {
            JArray weathers = (JArray)forecast.SelectToken("weather");
            JObject weatherObject = (JObject)weathers.First();
            string weather = (string)weatherObject.SelectToken("main");
            return weather;
        }

    }
}
