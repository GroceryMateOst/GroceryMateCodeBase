using grocery_mate_backend.Sandbox;

namespace grocery_mate_backend.Models;

public class EMailAddress
{
    public Guid EMailAddressId { get; set; }
    public string UserName { get; }
    public string DomainName { get; }
    public string CountryCode { get; }

    public string FullMailAddress => UserName + Symbols.At + DomainName + Symbols.Dot + CountryCode;

    public EMailAddress(string userName, string domainName, string countryCode)
    {
        UserName = userName;
        DomainName = domainName;
        CountryCode = countryCode;
    }

    public EMailAddress(string fullMailAddress)
    {
        UserName = Symbols.Empty;
        DomainName = Symbols.Empty;
        CountryCode = Symbols.Empty;
        
        try
        {
            var indexOfAt = fullMailAddress.IndexOf(Symbols.At, StringComparison.Ordinal);
            var spitedMail = fullMailAddress.Split(Symbols.At);


            UserName = fullMailAddress[..(indexOfAt)];
            DomainName = spitedMail[1].Split(Symbols.Dot)[0];
            CountryCode = spitedMail[1].Replace(DomainName + Symbols.Dot, Symbols.Empty);

            if (UserName.Length <= 0 || DomainName.Length <= 0 || CountryCode.Length <= 1 || !spitedMail[1].Contains(Symbols.Dot))
            {
                throw new Exception();
            }
        }
        catch (Exception)
        {
            throw new InvalidEmailAddressException();
        }
    }
}

public class InvalidEmailAddressException : Exception
{
    public InvalidEmailAddressException() : base("Invalid Mail Address!")
    {
    }
}