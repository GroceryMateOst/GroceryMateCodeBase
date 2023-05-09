using System.Net.Mail;
using grocery_mate_backend.BusinessLogic.Notification.Mail;
using grocery_mate_backend.Models.Authentication;
using Moq;
using NUnit.Framework;

namespace grocery_mate_backend_Test.Unit.BusinesLogic.Notification.Mail;

public class MailNotifierTest
{
    private MailNotifier _mailNotifier;
    private Mock<SmtpClient> _smtpClientMock;
    private Mock<MailMessage> _mailMessageMock;

    public MailNotifierTest()
    {
        _mailNotifier = new MailNotifier();
    }

    [SetUp]
    public void SetUp()
    {
        _smtpClientMock = new Mock<SmtpClient>();
    }

    [Test]
    public async Task SendMailNotification__true()
    {
        // Arrange
        const string fromMailAddress = "anna.meier@msn.com";
        const string toMailAddress = "hans.peter@gmail.com";
        const string subject = "Test Subject";
        const string message = "Test Body";
        const int port = 123;
        const string host = "localhost";
        const string pw = "test123";

        var mailConfigs = new MailConfigs(toMailAddress, subject, message);
        var appSettings = new AppSettingsGMailDto(fromMailAddress, host, port, pw);

        // Act
        var result = MailNotifier.SendMailNotification(mailConfigs, appSettings);

        // Assert
        // _smtpClientMock.Verify(s => s.Send(new MailMessage()), Times.Once);
    }
    
    [Test]
    public async Task SendMailNotification_HotTest_true()
    {
        // Arrange
        const string fromMailAddress = "grocerymateost@gmail.com";
        const string toMailAddress = "marc.kissling@creamec.com";
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