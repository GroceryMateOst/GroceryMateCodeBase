using grocery_mate_backend.Sandbox;

namespace grocery_mate_backend.Models;

public class BillingInformation
{
    public User CardOwner { get; }
    public IBAN Iban { get; }

    public BillingInformation(User cardOwner, IBAN iban)
    {
        CardOwner = cardOwner;
        Iban = iban;
    }
}

public class IBAN
{
    // 2-digit country code
    public string CountryCode { get; private set; }

    // 2-digit check number

    public int ControlDigits { get; private set; }

    // 5 characters from the bank code of the bank

    public int BankCode { get; private set; }

    // 12-digit code for the account number
    public int AccountNumber { get; private set; }

    public string FullIban
    {
        get { return CountryCode + ControlDigits + BankCode + AccountNumber; }
    }

    public IBAN(string countryCode, int controlDigits, int bankCode, int accountNumber)
    {
        CountryCode = countryCode;
        ControlDigits = controlDigits;
        BankCode = bankCode;
        AccountNumber = accountNumber;
    }

    public IBAN(string fullIban)
    {
        CountryCode = fullIban[..2];
        ControlDigits = Convert.ToInt32(fullIban.Substring(2,2));
        BankCode = Convert.ToInt32(fullIban.Substring(5,10));
        AccountNumber = Convert.ToInt32(fullIban[11..]);
    }
}

public class IbanException : Exception
{
    public IbanException(string? message) : base(message)
    {
        message = "The given Number-length is not a valid!";
    }
}