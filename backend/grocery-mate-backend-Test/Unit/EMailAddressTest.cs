using grocery_mate_backend;
using grocery_mate_backend.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using NUnit.Framework;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace grocery_mate_backend_Test.Unit;

public class EMailAddressTest
{
    private EMailAddress _EMailAddress;

    [Xunit.Theory]
    [InlineData("hans.peter@ost.ch", "hans.peter", "ost" , "ch")]
    [InlineData("hanspeter@ost.ch", "hanspeter", "ost" , "ch")]
    [InlineData("hans.peter@ost.com", "hans.peter", "ost" , "com")]
    [InlineData("hanspeter@ost.com", "hanspeter", "ost" , "com")]
    [InlineData("hans.peter@ost.co.uk", "hans.peter", "ost" , "co.uk")]
    [InlineData("hanspeter@ost.co.uk", "hanspeter", "ost" , "co.uk")]
    public void ConstructorWithFullAddress_ValidAddress_true(string fullMailAddress, string expUserName, string expDomainName, string expCountryCode)
    {
        _EMailAddress = new EMailAddress(fullMailAddress);

        var userName = _EMailAddress.UserName;
        var domainName = _EMailAddress.DomainName;
        var countryCode = _EMailAddress.CountryCode;
        
        Assert.Multiple(() =>
        {
            Assert.That(userName, Is.EqualTo(expUserName));
            Assert.That(domainName, Is.EqualTo(expDomainName));
            Assert.That(countryCode, Is.EqualTo(expCountryCode));
        });
    }
    
    [Xunit.Theory]
    [InlineData("hans.peterost.co.uk")]
    [InlineData("hanspeter@ost")]
    [InlineData("@ost.com")]
    [InlineData(" ")]
    [InlineData("")]
    public void ConstructorWithFullAddress_InvalidAddress_InvalidEmailAddressException(string fullMailAddress)
    {
        Assert.Multiple(() =>
        {
            Assert.That(() =>new EMailAddress(fullMailAddress), 
                Throws.Exception
                    .TypeOf<InvalidEmailAddressException>());
            
        });
    }
}