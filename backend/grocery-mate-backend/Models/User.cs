namespace grocery_mate_backend.Models;

public class User
{
    public String FirstName { get;  }
    public String SecondName { get; }
    public String EMailAddress { get;  }
    public BillingInformation BillingInformation { get; set; }

    public String FullName
    {
        get { return FirstName + " " + SecondName; }
    }

    public User(string firstName, string secondName, string eMailAddress)
    {
        FirstName = firstName;
        SecondName = secondName;
        EMailAddress = eMailAddress;
    }

    public User(string firstName, string secondName, string eMailAddress, BillingInformation billingInformation)
    {
        FirstName = firstName;
        SecondName = secondName;
        EMailAddress = eMailAddress;
        BillingInformation = billingInformation;
    }
}