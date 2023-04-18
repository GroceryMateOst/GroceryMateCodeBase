using grocery_mate_backend.Utility.Log;
using Microsoft.IdentityModel.Tokens;

namespace grocery_mate_backend.BusinessLogic.Validation.UserSettings;

public static class UserValidation
{
    public static bool ValidateUser(Object user, string methodName)
    {
        if (user != null) return true;
        GmLogger.GetInstance()?.Warn(methodName, "User with given eMail-Adr. not found");
        return false;
    }

    public static bool ValidateUserMail(string mailAddress, string methodName)
    {
        if (!mailAddress.IsNullOrEmpty()) return true;
        GmLogger.GetInstance()?.Warn(methodName, "Invalid Mail-Address");
        return false;
    }
}