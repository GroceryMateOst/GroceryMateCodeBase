using System.Net.Mail;
using grocery_mate_backend.BusinessLogic.Notification.Mail;
using grocery_mate_backend.Models.Authentication;
using Moq;
using NUnit.Framework;

namespace grocery_mate_backend_Test.Unit.BusinesLogic.Notification.Mail;

public class MailNotifierTest
{
  
    [Test]
    public void SendMailNotification_ExceptionThrown_ReturnsFalse()
    {
        // Arrange
        var configs = new MailConfigs("recipient@example.com", "Test Subject", "Test Message");
        var appSettings = new AppSettingsGMailDto("sender@example.com", "localhost", 123, "password");
        
        // _smtpClientMock.Setup(m => m.Send(It.IsAny<MailMessage>())).Throws<Exception>();

        // Act
        var result = MailNotifier.SendMailNotification(configs, appSettings);

        // Assert
        Assert.That(result, Is.False);
    }
    
    
    // [Test]
    private async Task SendMailNotification_HotTest_true()
    {
        // Arrange
        const string fromMailAddress = "grocerymateost@gmail.com";
        const string toMailAddress = "marc.kissling@ost.com";
        const string subject = "GroceryMate Bestelung";
        const string message = "<p>Dies ist ein Test <br> htest</p> ";
        const int port = 587;
        const string host = "smtp.gmail.com";
        const string pw = "sbhjqctqnkvmqlpq";

        var mailConfigs = new MailConfigs(toMailAddress, subject, message);
        var appSettings = new AppSettingsGMailDto(fromMailAddress, host, port, pw);

        // Act
        var result = MailNotifier.SendMailNotification(mailConfigs, appSettings);
    }
}