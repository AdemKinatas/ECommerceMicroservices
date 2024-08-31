using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
namespace Shared.Logging;

public static class LoggingService
{
    private static readonly Logger logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

    public static ILoggerFactory AddNLogLogging(this ILoggerFactory loggerFactory)
    {
        loggerFactory.AddNLog();
        return loggerFactory;
    }

    public static void LogError(Exception exception, string message)
    {
        logger.Error(exception, message);
    }

    public static void LogInfo(string message)
    {
        logger.Info(message);
    }
}
