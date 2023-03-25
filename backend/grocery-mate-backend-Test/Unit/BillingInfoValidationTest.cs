using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.Models;
using NUnit.Framework;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace grocery_mate_backend_Test.Unit;

public class BillingInfoValidationTest
{
    private IBAN _iban;

 //    [Xunit.Theory]
 //    [InlineData("CH93 0076 2011 6238 5295 7", "CH", "93", "00762", "011623852957")]
 //    [InlineData("AL47 2121 1009 0000 0002 3569 8741", "AL", "47", "ch")]
 //    public void IbanConstructorWithFullIban_ValidIban_true(string fullIBAN,
 //     string expCountryCode, string expControlDigits, string expBankCode, string expAccountNumber)
 //    {
 //        _iban = BillingInfoValidation.IbanValidation(fullIBAN);
 //
 //        var countryCode = _iban.CountryCode;
 //        var controlDigits = _iban.ControlDigits;
 //        var bankCode = _iban.BankCode;
 //        var accountNumber = _iban.AccountNumber;
 //        
 //        Assert.Multiple(() =>
 //        {
 //            Assert.That(countryCode, Is.EqualTo(expCountryCode));
 //            Assert.That(expControlDigits, Is.EqualTo(expControlDigits));
 //          //  Assert.That(expBankCode, Is.EqualTo(bankCode));
 //          //  Assert.That(accountNumber, Is.EqualTo(expAccountNumber));
 //            
 //        });
 //    }

 //   [Xunit.Theory]
 //   [InlineData("hans.peter@ost.ch", "hans.peter", "ost", "ch")]
 //   public void IbanConstructorWithFullIban_InvalidIban_Exception(string fullMailAddress, string expUserName,
 //       string expDomainName, string expCountryCode)
 //   {
 //       _iban = new IBAN("");
 //   }
}