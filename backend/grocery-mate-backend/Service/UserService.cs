using grocery_mate_backend.BusinessLogic.Validation.Shopping;
using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Utility.Log;

namespace grocery_mate_backend.Service;

public class UserService
{
    public static async Task<User?> GetAuthenticatedUser(string? identityName, IUnitOfWork unitOfWork)
    {
        const string methodName = "Get Authenticated User";
        
        if (identityName == null)
        {
            GmLogger.GetInstance()?.Warn(methodName, "Couldn't find user by give JWT-Token");
            return null;
        }
        
        var id = (await unitOfWork.Authentication.FindIdentityUser(identityName)).Id;
        if (id == null)
        {
            GmLogger.GetInstance()?.Warn(methodName, "User with given identityId not found");
            return null;
        }

        var user = await unitOfWork.User.FindUserByIdentityId(id);
        if (user == null)
        {
            GmLogger.GetInstance()?.Warn(methodName, "User with given identityId does not exist");
            return null;
        }

        return user;
    }
}