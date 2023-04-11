using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models;
using NUnit.Framework;

namespace grocery_mate_backend_Test.Unit.Endpoint;

public partial class ServiceIntegrationTests
{
    [Test]
    public void FindUserTest_BasedOnEmail_true()
    {
        var result = _userService.FindUser("hans.mustermann@gmail.com");
        Assert.Multiple(() =>
        {
            Assert.That(result.Result?.FirstName, Is.EqualTo("Hans"));
            Assert.That(result.Result?.SecondName, Is.EqualTo("Mustermann"));
        });
    }

    [Test]
    public void FindUserTest_BasedOnEmail_false()
    {
        var result = _userService.FindUser("hans.amman@gmail.com");
        Assert.That(result.Result, Is.EqualTo(null));
    }
}