using grocery_mate_backend.Utility.Log;
using Microsoft.IdentityModel.Tokens;

namespace grocery_mate_backend.BusinessLogic.Validation;

public abstract class UserValidation : ValidationBase
{
    public static bool ValidateUser(object? user)
    {
        return Validate(user,
            "ValidateUser",
            "User with given eMail-Adr. not found",
            item => item != null);
    }
    
    public static bool ValidateUserMail(string mailAddress)
    {
        return Validate(mailAddress,
            "ValidateUserMail",
            "Invalid Mail-Address",
            item => !item.IsNullOrEmpty());
    }
}