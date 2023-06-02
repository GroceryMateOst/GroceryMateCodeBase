using System.Net;
using System.Net.Mail;
using grocery_mate_backend.Models.Authentication;
using grocery_mate_backend.Utility;
using grocery_mate_backend.Utility.Log;

namespace grocery_mate_backend.BusinessLogic.Notification.Mail;

public class MailNotifier
{
    

    public static bool SendMailNotification(MailConfigs configs, AppSettingsGMailDto appSettings)
    {
        var message = new MailMessage();
        var smtpClient = new SmtpClient();

        message.From = new MailAddress(appSettings.MailFromAddress);
        message.To.Add(new MailAddress(configs.ToAddress.ToString()));
        message.Subject = configs.Subject;
        message.Body = configs.Message;
        message.IsBodyHtml = true;

        smtpClient.Port = appSettings.MailPort;
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Host = appSettings.MailHost;
        smtpClient.Credentials = new NetworkCredential(appSettings.MailFromAddress, appSettings.MailPassword);

        try
        {
            smtpClient.Send(message);
        }
        catch (Exception e)
        {
            GmLogger.Instance.Warn(LogMessages.Notification_Mail_Send, LogMessages.LogMessage_ErrorMailSend);
            return false;
        }

        return true;
    }
}