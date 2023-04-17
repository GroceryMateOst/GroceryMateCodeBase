using grocery_mate_backend.Data.DataModels.UserManagement;

namespace grocery_mate_backend.Controllers.Repo.Settings;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> FindUserByMail(string email);
}