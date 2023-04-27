using grocery_mate_backend.Controllers.Repo.Generic;
using grocery_mate_backend.Data.DataModels.Authentication;

namespace grocery_mate_backend.Controllers.Repo.Authentication;


public interface ICanceledTokensRepository : IGenericRepository<TokenBlacklistEntry>
{
    Task<bool> AddTokenToBlacklist(string token);
    Task<bool> ValidateToken(string token);
}