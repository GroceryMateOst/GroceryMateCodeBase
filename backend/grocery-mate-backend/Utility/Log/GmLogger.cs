
using NLog;
using ILogger = grocery_mate_backend.Services.Utility.ILogger;

namespace grocery_mate_backend.Utility.Log;

public class GmLogger : ILogger
{
    private static GmLogger? _instance;
    private static Logger? _logger;

    public static GmLogger? GetInstance()
    {
        if (_instance == null)
            _instance = new GmLogger();
        return _instance;
    }

    private static Logger? GetLogger(string logger)
    {
        if (_logger == null)
            _logger = LogManager.GetLogger(logger);
        return _logger;
    }

    public void Trace(string method, string message, string? arg = null)
    {
        GetLogger("groceryMateLoggerRule")?.Trace(GenerateLogMsg(method, message, arg));
    }

    public void Debug(string method, string message, string? arg = null)
    {
        GetLogger("groceryMateLoggerRule")?.Debug(GenerateLogMsg(method, message, arg));
    }


    public void Info(string method, string message, string? arg = null)
    {
        GetLogger("groceryMateLoggerRule")
            ?.Info($" {GenerateLogMsg(method, message, arg)}");
    }


    public void Warn(string method, string message, string? arg = null)
    {
        GetLogger("groceryMateLoggerRule")?.Warn($" {GenerateLogMsg(method, message, arg)}" );
    }

    public void Error(string method, string message, string? arg = null)
    {
        GetLogger("groceryMateLoggerRule")?.Error(GenerateLogMsg(method, message, arg));
    }

    private string GenerateLogMsg(string method, string message, string? arg = null)
    {
        return arg == null
            ? $" [{method}] {message}"
            : $" [{method}] {message} Arg: {arg}";
    }
}