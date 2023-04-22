using grocery_mate_backend.Controllers.Repo.Generic;
using grocery_mate_backend.Data.Context;
using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Data.DataModels.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace grocery_mate_backend.Controllers.Repo.Shopping;

public class ShoppingRepository : GenericRepository<GroceryRequest>, IShoppingRepository
{
    private readonly GroceryContext _context;

    public ShoppingRepository(GroceryContext context) : base(context)
    {
        _context = context;
    }
    
    public Task<List<GroceryRequest>> GetAllGroceryRequests()
    {
        return Task.FromResult(_context.GroceryRequests
            .Where(req => req.State == GroceryRequestState.Published)
            .Include(request => request.Client)
            .Include(request => request.ShoppingList!.Items)
            .OrderBy(request => request.ToDate)
            .Take(10)
            .ToList());
    }

    public async Task<bool> Add(GroceryRequest request, User user)
    {
        _context.Attach(user);
        user.GroceryRequestsClients?.Add(request);
        await _context.SaveChangesAsync();
        return true;
    }
}