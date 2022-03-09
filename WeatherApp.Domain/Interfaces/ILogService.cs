namespace WeatherApp.Domain.Interfaces
{
    public interface ILogService
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(Exception ex, string message);
    }
}
