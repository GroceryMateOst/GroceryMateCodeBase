using grocery_mate_backend.Models.Authentication;

namespace grocery_mate_backend.BusinessLogic.Notification.Mail;

public abstract class MailNotification
{
    private delegate bool ShoppingRequestAcceptedNotificationDelegate(
        string mailAddress,
        string fullName,
        string mailSubjectTemplate,
        string mailBodyTemplate,
        AppSettingsGMailDto appSettings);


    public static void ShoppingRequestAcceptedNotificationForClient(
        string clientMailAddress,
        string contractorFullName,
        AppSettingsGMailDto appSettings)
    {
        ShoppingRequestNotification(
            clientMailAddress,
            contractorFullName,
            NotificationMessages.Mail_Subject_RequestAccepted_Client,
            NotificationMessages.Mail_Body_RequestAccepted_Client,
            appSettings);
    }

    public static void ShoppingRequestAcceptedNotificationForContractor(
        string contractorMailAddress,
        string clientFullName,
        AppSettingsGMailDto appSettings)
    {
        ShoppingRequestNotification(
            contractorMailAddress,
            clientFullName,
            NotificationMessages.Mail_Subject_RequestAccepted_Contractor,
            NotificationMessages.Mail_Body_RequestAccepted_Contractor,
            appSettings);
    }


    public static void ShoppingRequestFulfilledNotificationForClient(
        string clientMailAddress,
        string contractorFullName,
        AppSettingsGMailDto appSettings)
    {
        ShoppingRequestNotification(
            clientMailAddress,
            contractorFullName,
            NotificationMessages.Mail_Subject_RequestFulfilled_Client,
            NotificationMessages.Mail_Body_RequestFulfilled_Client,
            appSettings);
    }

    public static void ShoppingRequestFulfilledNotificationForContractor(
        string contractorMailAddress,
        string clientFullName,
        AppSettingsGMailDto appSettings)
    {
        ShoppingRequestNotification(
            contractorMailAddress,
            clientFullName,
            NotificationMessages.Mail_Subject_RequestFulfilled_Contractor,
            NotificationMessages.Mail_Body_RequestFulfilled_Contractor,
            appSettings);
    }


    private static ShoppingRequestAcceptedNotificationDelegate ShoppingRequestNotification { get; } =
        (mailAddress, fullName, mailSubjectTemplate, mailBodyTemplate, appSettings) =>
        {
            var mailBody = string.Format(mailBodyTemplate, fullName);

            return MailNotifier.SendMailNotification(
                new MailConfigs(
                    mailAddress,
                    mailSubjectTemplate,
                    mailBody),
                appSettings);
        };
}