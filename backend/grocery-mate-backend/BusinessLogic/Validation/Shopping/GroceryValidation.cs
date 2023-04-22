using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models.Shopping;
using grocery_mate_backend.Utility.Log;

namespace grocery_mate_backend.BusinessLogic.Validation.Shopping;

public static class GroceryValidation
{
    public static bool ValidateRequestState(string requestState)
    {
        return requestState is "unpublished" or "published";
    }
}
