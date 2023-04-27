using grocery_mate_backend.Controllers.Repo.Authentication;
using grocery_mate_backend.Data.DataModels.Authentication;
using grocery_mate_backend.Data.DataModels.UserManagement.Address;
using grocery_mate_backend.Utility.Log;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace grocery_mate_backend.BusinessLogic.Validation;

public class ValidationBase
{
    private delegate bool GroceryPredicate<in T>(T item);

    public static bool ValidateModel(ModelStateDictionary modelState)
    {
        return ValidateModelState(modelState);
    }

    public static bool ValidateModel(ModelStateDictionary modelState,
        IHeaderDictionary headers, ICanceledTokensRepository canceledTokensRepository)
    {
        var token =  headers["Authorization"].ToString().Replace("Bearer ", "");
        return ValidateModelState(modelState) &&
               ValidateSessionToken(token, canceledTokensRepository);
    }

    private static bool ValidateModelState(ModelStateDictionary modelState)
    {
        return Validate(modelState,
            "ValidateModelState",
            "Invalid Model-State due to Bad credentials",
            item => item.IsValid);
    }


    private static bool ValidateSessionToken(string token,
        ICanceledTokensRepository canceledTokensRepository)
    {
        return Validate(canceledTokensRepository.ValidateToken(token),
            "ValidateModelState",
            "Invalid Model-State due to Bad credentials",
            item => item.Result);
    }

    public static bool ValidateAddress(Address? address, string methodName)
    {
        if (address != null && address.AddressId != Guid.Empty) return true;
        GmLogger.Instance.Warn(methodName, "User with given eMail-Adr. not found");
        return false;
    }

    private static bool Validate<T>(T thing, string methodName, string errorMsg, GroceryPredicate<T> predicate)
    {
        if (predicate(thing)) return true;
        GmLogger.Instance.Warn(methodName, errorMsg);
        return false;
    }
}