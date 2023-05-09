using grocery_mate_backend.Models.Authentication;

namespace grocery_mate_backend.BusinessLogic.Notification;

public interface IUserNotification
{
    public static bool ShoppingRequestAcceptedNotification(string clientMailConfigs, string contractorMailConfigs,         AppSettingsGMailDto appSettings)
    {
        throw new NotImplementedException();
    }

    public static bool ShoppingRequestFulfilledNotification(string clientMailConfigs, string contractorMailConfigs)
    {
        throw new NotImplementedException();
    }
}