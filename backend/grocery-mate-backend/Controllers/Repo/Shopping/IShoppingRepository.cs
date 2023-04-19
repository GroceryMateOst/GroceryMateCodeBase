using grocery_mate_backend.Controllers.Repo.Generic;
using grocery_mate_backend.Data.DataModels.Shopping;

namespace grocery_mate_backend.Controllers.Repo.Shopping;

public interface IShoppingRepository : IGenericRepository<GroceryRequest>
{
    Task<GroceryRequest?> FindGroceryRequest(string clientMail, string contractorMail);
}