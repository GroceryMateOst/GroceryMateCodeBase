using grocery_mate_backend.Models;
using grocery_mate_backend.Services.Utility;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace grocery_mate_backend.BusinessLogic.Validation;

public class ValidationBase
{
    protected static bool ValidateModelState(ModelStateDictionary modelState, string methodName)
    {
        if (modelState.IsValid) return true;
        GmLogger.GetInstance()?.Warn(methodName, "Invalid Model-State due to Bad credentials");
        return false;
    }

    protected internal static bool ValidateUser(Object user, string methodName)
    {
        if (user != null) return true;
        GmLogger.GetInstance()?.Warn(methodName, "User with given eMail-Adr. not found");
        return false;
    }

    protected internal static bool ValidateAddress(Address address, string methodName)
    {
        if (address != null || address.AddressId != Guid.Empty) return true;
        GmLogger.GetInstance()?.Warn(methodName, "User with given eMail-Adr. not found");
        return false;
    }
}