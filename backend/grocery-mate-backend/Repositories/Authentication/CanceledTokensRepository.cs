using grocery_mate_backend.Controllers.Repo.Authentication;
using grocery_mate_backend.Controllers.Repo.Generic;
using grocery_mate_backend.Data.Context;
using grocery_mate_backend.Data.DataModels.Authentication;
using grocery_mate_backend.Utility.Log;

namespace grocery_mate_backend.Repositories.Authentication;

public class CanceledTokensRepository : GenericRepository<TokenBlacklistEntry>, ICanceledTokensRepository
{
    private readonly GroceryContext _context;
    private static int _expirationMinutes = 1;
    private IGenericRepository<TokenBlacklistEntry> _genericRepositoryImplementation;

    public CanceledTokensRepository(GroceryContext context, int expirationMinutes) : base(context)
    {
        _context = context;
        _expirationMinutes = expirationMinutes;
    }


    public async Task<bool> AddTokenToBlacklist(string token)
    {
        try
        {
            var blacklistEntry = new TokenBlacklistEntry(token, DateTime.UtcNow);
            _context.CanceledTokens.Add(blacklistEntry);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            GmLogger.Instance.Trace(LogMessages.MethodName_REPO_CanceledTokens, e.Message);
            return false;
        }

        await RemoveOldTokensFromBlacklist();
        return true;
    }

    private async Task RemoveOldTokensFromBlacklist()
    {
        var expiredTokens = _context.CanceledTokens.Where(entry =>
            0 < DateTime.UtcNow.CompareTo(entry.CancellationDate.AddMinutes(2 * _expirationMinutes)));
        foreach (var token in expiredTokens)
        {
            _context.CanceledTokens.Remove(token);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ValidateToken(string token)
    {
        try
        {
            if (_context.CanceledTokens.Any(t => t.CanceledToken == token))
            {
                GmLogger.Instance.Trace(LogMessages.MethodName_REPO_CanceledTokens, LogMessages.LogMessage_TokenCanceled);
                return false;
            }
        }
        catch (Exception)
        {
            GmLogger.Instance.Trace(LogMessages.MethodName_REPO_CanceledTokens, LogMessages.LogMessage_TokenCheckFailed);
            return false;
        }

        return true;
    }
}