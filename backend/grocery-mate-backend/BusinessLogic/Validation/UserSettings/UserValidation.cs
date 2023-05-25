using grocery_mate_backend.Utility;
using grocery_mate_backend.Utility.Log;
using Microsoft.IdentityModel.Tokens;

namespace grocery_mate_backend.BusinessLogic.Validation;

public abstract class UserValidation : ValidationBase
{
    public static bool ValidateUser(object? user)
    {
        return Validate(user,
            ErrorMessages.Address_userNotFound,
            item => item != null);
    }
    
    public static bool ValidateUserMail(string mailAddress)
    {
        return Validate(mailAddress,
            ErrorMessages.UserMail_invalid,
            item => !item.IsNullOrEmpty());
    }
}