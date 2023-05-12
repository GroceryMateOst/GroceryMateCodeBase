namespace grocery_mate_backend.Models.Authentication;

public class AppSettingsGMailDto
{
    public string MailFromAddress {get; set; }
    public string MailHost {get; set; }
    public int MailPort {get; set; }
    public string MailPassword {get; set; }

    public AppSettingsGMailDto(string mailFromAddress, string mailHost, int mailPort, string mailPassword)
    {
        MailFromAddress = mailFromAddress;
        MailHost = mailHost;
        MailPort = mailPort;
        MailPassword = mailPassword;
    }
}