namespace grocery_mate_backend.Services.Utility;

public interface ILogger
{
    void Trace(string method, string message, string? arg = null);
    void Debug(string restMethod, string message, string? arg = null);
    void Info(string restMethod, string message, string? arg = null);
    void Warn(string restMethod, string message, string? arg = null);
    void Error(string restMethod, string message, string? arg = null);
}