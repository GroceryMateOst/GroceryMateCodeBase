using NLog;
using ILogger = grocery_mate_backend.Services.Utility.ILogger;

namespace grocery_mate_backend.Utility.Log;

public class GmLogger : ILogger
{
    private static readonly Lazy<GmLogger> lazy =
        new Lazy<GmLogger>(() => new GmLogger());

    private readonly Logger logger;

    public static GmLogger Instance => lazy.Value;

    /**
     * Log-Directory: GroceryMateCodeBase\backend\grocery-mate-backend\bin\Debug\net6.0\logs 
     */
    private GmLogger()
    {
        // Logger for Log-File
        logger = LogManager.GetLogger("groceryMateLoggerFileRule");
        
        // Logger for Console
        // logger = LogManager.GetLogger("groceryMateLoggerConsoleRule");
    }

    public void Trace(string method, string message, string? arg = null)
    {
        logger.Trace(GenerateLogMsg(method, message, arg));
    }
    public void Debug(string method, string message, string? arg = null)
    {
        logger.Debug(GenerateLogMsg(method, message, arg));
    }

    public void Info(string method, string message, string? arg = null)
    {
        logger.Info(GenerateLogMsg(method, message, arg));
    }

    public void Warn(string method, string message, string? arg = null)
    {
        logger.Warn(GenerateLogMsg(method, message, arg));
    }

    public void Error(string method, string message, string? arg = null)
    {
        logger.Error(GenerateLogMsg(method, message, arg));
    }

    private static string GenerateLogMsg(string method, string message, string? arg = null)
    {
        return arg == null
            ? $" [{method}] {message}"
            : $" [{method}] {message} Arg: {arg}";
    }
}