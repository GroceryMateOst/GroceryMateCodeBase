using grocery_mate_backend.Controllers.Repo.Generic;
using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Data.DataModels.UserManagement;

namespace grocery_mate_backend.Controllers.Repo.Shopping;

public interface IShoppingRepository : IGenericRepository<GroceryRequest>
{
    Task<List<GroceryRequest>> GetAllGroceryRequests();
    Task<bool> Add(GroceryRequest entity, User user);
}