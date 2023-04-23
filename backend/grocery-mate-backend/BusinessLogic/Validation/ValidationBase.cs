using grocery_mate_backend.Data.DataModels.UserManagement.Address;
using grocery_mate_backend.Utility.Log;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace grocery_mate_backend.BusinessLogic.Validation;

public class ValidationBase
{
    public static bool ValidateModelState(ModelStateDictionary modelState, string methodName)
    {
        if (modelState.IsValid) return true;
        GmLogger.GetInstance()?.Warn(methodName, "Invalid Model-State due to Bad credentials");
        return false;
    }

    public static bool ValidateAddress(Address? address, string methodName)
    {
        if (address != null && address.AddressId != Guid.Empty) return true;
        GmLogger.GetInstance()?.Warn(methodName, "User with given eMail-Adr. not found");
        return false;
    }
}