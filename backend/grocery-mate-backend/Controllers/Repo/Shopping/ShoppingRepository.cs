using grocery_mate_backend.Controllers.Repo.Generic;
using grocery_mate_backend.Data.Context;
using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Utility.Log;
using Microsoft.EntityFrameworkCore;

namespace grocery_mate_backend.Controllers.Repo.Shopping;

public class ShoppingRepository : GenericRepository<GroceryRequest>, IShoppingRepository
{
    private readonly GroceryContext _context;

    public ShoppingRepository(GroceryContext context) : base(context)
    {
        _context = context;
    }

    public Task<GroceryRequest?> FindGroceryRequest(string clientMail, string contractorMail)
    {
        try
        {
            return _context.GroceryRequests
                .Where(
                    u =>
                        u.Client.EmailAddress == clientMail &&
                        u.Contractor.EmailAddress == contractorMail)
                .FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            GmLogger.GetInstance()?.Warn("SettingsRepository: ", "No matching Request found!");
            return Task.FromResult<GroceryRequest?>(new GroceryRequest());
        }
    }
}