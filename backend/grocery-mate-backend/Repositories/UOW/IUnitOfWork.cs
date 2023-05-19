using grocery_mate_backend.Controllers.Repo.Authentication;
using grocery_mate_backend.Controllers.Repo.Settings;
using grocery_mate_backend.Controllers.Repo.Shopping;
using grocery_mate_backend.Repositories.Messaging;

namespace grocery_mate_backend.Controllers.Repo.UOW;

public abstract class IUnitOfWork
{
    public abstract IAuthenticationRepository Authentication { get; }
    public abstract ICanceledTokensRepository TokenBlacklist { get; }
    public abstract IUserRepository User { get; }
    public abstract IShoppingRepository Shopping { get; }
    public abstract IAddressRepository Address { get; }
    public abstract IMessagingRepository Messaging{ get; }
    public abstract Task CompleteAsync();
    public abstract void Dispose();
}