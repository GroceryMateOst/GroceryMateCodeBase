using grocery_mate_backend.Controllers.Repo.Generic;
using grocery_mate_backend.Data.Context;
using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Service;
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
        return GetGroceryRequests();
    }

    public async Task<List<GroceryRequest>> GetAllGroceryRequestsByZipcode(int zipcode)
    {
        if (zipcode == 0) return await GetGroceryRequests();
        var coordinates = await GeoApifyApi.GetCoordinatesByZipCode(zipcode);
        var groceryRequests = await Task.FromResult(_context.GroceryRequests
            .Include(req => req.Client.Address)
            .ToList());

        foreach (var groceryRequest in groceryRequests)
        {
            if (groceryRequest.Client.Address == null) continue;
            var distance = DistanceCalculationService.CalculateDistance(
                coordinates.lat,
                coordinates.lon,
                groceryRequest.Client.Address.Latitude,
                groceryRequest.Client.Address.Longitude);
            groceryRequest.Distance = Convert.ToDecimal(Math.Truncate(distance / 100) / 10);
        }
        return groceryRequests.OrderBy(request => request.Distance).ToList();
    }

    public Task<List<GroceryRequest>> GetGroceryRequestsAsContractor(User user)
    {
        return Task.FromResult(_context.GroceryRequests
            .Where(req => req.Contractor == user)
            .Where(req => req.State == GroceryRequestState.Accepted)
            .Include(request => request.ShoppingList.Items)
            .Include(request => request.Client.Address)
            .Include(request => request.Contractor.Address)
            .ToList());
    }

    public Task<List<GroceryRequest>> GetGroceryRequestsAsClient(User user)
    {
        return Task.FromResult(_context.GroceryRequests
            .Where(req => req.Client == user)
            .Where(req => req.State == GroceryRequestState.Published)
            .Include(request => request.ShoppingList.Items)
            .Include(request => request.Client.Address)
            .Include(request => request.Contractor.Address)
            .ToList());
    }

    public async Task<bool> Add(GroceryRequest request, User user)
    {
        _context.Attach(user);
        user.GroceryRequestsClients?.Add(request);
        await _context.SaveChangesAsync();
        return true;
    }

    private Task<List<GroceryRequest>> GetGroceryRequests()
    {
        return Task.FromResult(_context.GroceryRequests
            .Where(req => req.State == GroceryRequestState.Published)
            .Include(request => request.Client)
            .Include(request => request.ShoppingList.Items)
            .OrderBy(request => request.ToDate)
            .ToList());
    }
}