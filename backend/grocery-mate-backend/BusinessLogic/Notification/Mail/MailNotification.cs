using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models.Authentication;

namespace grocery_mate_backend.BusinessLogic.Notification.Mail;

public abstract class MailNotification : IUserNotification
{
    public static bool ShoppingRequestAcceptedNotification(
        string clientMailAddress,
        string contractorMailAddress, 
        AppSettingsGMailDto appSettings)
    {
        var mailBody = string.Format(NotificationMessages.Mail_Body_RequestAccepted_Client, contractorMailAddress);

        return MailNotifier.SendMailNotification(
            new MailConfigs(
                clientMailAddress,
                NotificationMessages.Mail_Subject_RequestAccepted_Client,
                mailBody), appSettings);
    }

    public static bool ShoppingRequestFulfilledNotification(
        string clientMailAddress,
        string contractorMailAddress)
    {
        var mailBody = string.Format(NotificationMessages.Mail_Body_RequestFulfilled_Client, contractorMailAddress);

        // return MailNotifier.SendMailNotification(
        //     new MailConfigs(
        //         clientMailAddress,
        //         NotificationMessages.Mail_Subject_RequestFulfilled_Client,
        //         mailBody));
        return false;
    }

   
}