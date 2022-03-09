using WeatherApp.Domain.Dto;

namespace WeatherApp.Domain.ServiceManager.Weather
{
    internal interface ICity
    {
        Task<List<Cities>> GetCitiesList(string cityName);

    }
}
