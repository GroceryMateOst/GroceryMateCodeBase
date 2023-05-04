using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Utility.Log;

namespace grocery_mate_backend.Service;

public class UserService
{
    public static async Task<User?> GetAuthenticatedUser(string? identityName, IUnitOfWork unitOfWork)
    {

        if (identityName == null)
        {
            GmLogger.Instance.Warn(LogMessages.MethodName_REST_UserFromToken, LogMessages.LogMessage_UserNotFoundByToken);
            return null;
        }

        var id = (await unitOfWork.Authentication.FindIdentityUser(identityName)).Id;
        if (id == null)
        {
            GmLogger.Instance.Warn(LogMessages.MethodName_REST_UserFromToken, LogMessages.LogMessage_UserNotFoundById);
            return null;
        }

        var user = await unitOfWork.User.FindUserByIdentityId(id);
        if (user != null) return user;
       
        GmLogger.Instance.Warn(LogMessages.MethodName_REST_UserFromToken, LogMessages.LogMessage_UserNotExistById);
       
        return null;
    }
}