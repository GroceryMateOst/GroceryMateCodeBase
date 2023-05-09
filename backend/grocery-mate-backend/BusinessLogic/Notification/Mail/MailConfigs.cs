namespace grocery_mate_backend.BusinessLogic.Notification.Mail;

public class MailConfigs : INotificationConfigs
{
    private string? _toMailAddress;

    public object? ToAddress
    {
        get => _toMailAddress;
        set => _toMailAddress = value as string;
    }

    public string Subject { get; set; }

    public string Message { get; set; }

    public MailConfigs(string? toMailAddress, string subject, string message)
    {
        _toMailAddress = toMailAddress;
        Subject = subject;
        Message = message;
    }
    
}