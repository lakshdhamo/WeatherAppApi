using Microsoft.Extensions.Logging;
using WeatherApp.Domain.Interfaces;

namespace WeatherApp.Domain.ServiceManager.LoggingManager
{
    public class LogService : ILogService
    {
        private readonly ILogger<LogService> _logger;
        public LogService(ILogger<LogService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Writes info log
        /// </summary>
        /// <param name="message"></param>
        public void LogInfo(string message)
        {
            _logger.LogInformation("Info: " + message + " - {date}", DateTime.Now);
        }

        /// <summary>
        /// Writes Warning log
        /// </summary>
        public void LogWarning(string message)
        {
            _logger.LogWarning("Waring: " + message + " - {date}", DateTime.Now);
        }

        /// <summary>
        /// Writes Error log
        /// </summary>
        public void LogError(Exception ex, string message)
        {
            _logger.LogError(ex, message + " - {date}", DateTime.Now);
        }
    }
}
