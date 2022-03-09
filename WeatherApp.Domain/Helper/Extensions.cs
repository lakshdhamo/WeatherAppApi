using System.Globalization;
using System.Net.Http.Headers;

namespace WeatherApp.Domain.Helper
{
    public static class Extensions
    {
        public static string ShortDateStringFormat(this DateTime date, string format)
        {
            return date.ToString(format, CultureInfo.InvariantCulture);
        }

        public static void UpdateBase(this HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://openweathermap.org/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }

    }
}
