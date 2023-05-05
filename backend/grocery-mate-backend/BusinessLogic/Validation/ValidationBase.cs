using System.Diagnostics;
using grocery_mate_backend.Controllers.Repo.Authentication;
using grocery_mate_backend.Data.DataModels.Authentication;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Utility.Log;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace grocery_mate_backend.BusinessLogic.Validation;

public class ValidationBase
{
    protected delegate bool GroceryPredicate<in T>(T item);

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
            "Invalid Model-State due to Bad credentials",
            item => item.IsValid);
    }

    public static bool ValidateSessionToken(string token,
        ICanceledTokensRepository canceledTokensRepository)
    {
        return Validate(canceledTokensRepository.ValidateToken(token),
            "Invalid Model-State due to Bad credentials",
            item => item.Result);
    }

    protected static bool Validate<T>(T thing, string errorMsg, GroceryPredicate<T> predicate)
    {
        if (predicate(thing)) return true;
        var methodName = new StackTrace().GetFrame(1)?.GetMethod()?.Name ?? "Validate";
        GmLogger.Instance.Warn(methodName,errorMsg);
        return false;
    }
}