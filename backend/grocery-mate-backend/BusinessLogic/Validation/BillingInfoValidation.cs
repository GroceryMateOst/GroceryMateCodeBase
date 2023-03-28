using grocery_mate_backend.Models;
using grocery_mate_backend.Sandbox;

namespace grocery_mate_backend.BusinessLogic.Validation;

public class BillingInfoValidation
{
    public static IBAN IbanValidation(string fullIban)
    {
        try
        {
            fullIban = fullIban.Replace(Symbols.Space, Symbols.Empty);

            if (fullIban.Length > 34)
            {
                throw new InvalidIbanException("IBAN-Nr. is to long! maximum length is 34 characters");
            }
            
            var countryCode = fullIban[..2];
            var controlDigits = Convert.ToInt32(fullIban.Substring(2, 2));
            var bankCode = Convert.ToInt32(fullIban.Substring(4, 5));
            var accountNumber = Convert.ToInt32(fullIban[9..]);

            return new IBAN(countryCode, controlDigits, bankCode, accountNumber);
        }
        catch (InvalidIbanException e)
        {
            throw new InvalidIbanException(e.Message);
        }
        catch (Exception e)
        {
            throw new InvalidIbanException();
        }
    }
}

public class InvalidIbanException : Exception
{
    public InvalidIbanException() : base("The given IBAN-Nr. is not a valid!")
    {
    }

    public InvalidIbanException(string message) : base(message)
    {
    }
}