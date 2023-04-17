using grocery_mate_backend.Controllers.Repo.Authentication;
using grocery_mate_backend.Controllers.Repo.Settings;
using grocery_mate_backend.Controllers.Repo.Shopping;

namespace grocery_mate_backend.Utility.UOW;

public interface IUnitOfWork
{
    IAuthenticationRepository Authentication { get; }
    IUserRepository User { get; }
    IShoppingRepository Shopping { get; }
    IAddressRepository Address { get; }
    Task CompleteAsync();
    void Dispose();
}