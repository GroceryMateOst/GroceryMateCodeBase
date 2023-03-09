using Microsoft.AspNetCore.Server.IIS.Core;

namespace grocery_mate_backend;

public class UnitExample
{
    public User MadeBy{ get; set;}

    public bool CanBeCanceled(User user)
    {
        if (user.IsAdmin  || MadeBy == user )
            return true;
        return MadeBy == user;
    }
}

public class User
{
    public bool IsAdmin { get; set; }
    public bool IsMale { get; set; }
}
