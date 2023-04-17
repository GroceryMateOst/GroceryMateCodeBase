using grocery_mate_backend.Services.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace grocery_mate_backend.BusinessLogic.Validation.Authentication;

public abstract class AuthenticationValidation : ValidationBase
{
    public new static bool ValidateModelState(ModelStateDictionary modelState, string methodName)
    {
        return ValidationBase.ValidateModelState(modelState, methodName);
    }

    public static bool ValidateIdentityUserCreation(IdentityResult? result, string methodName)
    {
        if (result is {Succeeded: true}) return true;
        foreach (var error in result.Errors)
        {
            GmLogger.GetInstance()?.Trace(methodName, error.Description);
        }

        GmLogger.GetInstance()?.Warn(methodName, "Creation of the Identity-User failed");
        return false;
    }

    public static bool ValidateUserPassword(bool result, string methodName)
    {
        if (result) return true;
        GmLogger.GetInstance()?.Warn(methodName, "Invalid Password");
        return false;
    }
}