using grocery_mate_backend.Utility.Log;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace grocery_mate_backend.BusinessLogic.Validation;

public abstract class AuthenticationValidation : ValidationBase
{
    public static bool ValidateIdentityUserCreation(IdentityResult? result, string methodName)
    {
        if (result is { Succeeded: true }) return true;
        foreach (var error in result.Errors)
        {
            GmLogger.Instance.Trace(methodName, error.Description);
        }

        GmLogger.Instance.Warn(methodName, "Creation of the Identity-User failed");
        return false;
    }
    
    public static bool ValidateUserPassword(bool result)
    {
        return Validate(result,
            "Invalid Password",
            item => item);
    }
}