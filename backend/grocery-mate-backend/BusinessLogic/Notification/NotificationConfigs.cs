namespace grocery_mate_backend.BusinessLogic.Notification;

public interface INotificationConfigs
{
    public object? ToAddress { get; set; }
    public string Message { get; set; }
}