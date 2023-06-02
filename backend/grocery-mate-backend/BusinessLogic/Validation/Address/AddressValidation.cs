using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Utility;

namespace grocery_mate_backend.BusinessLogic.Validation;

public abstract class AddressValidation : ValidationBase
{
    public static bool ValidateZipcode(int requestState)
    {
        return Validate(requestState,
            ErrorMessages.Zipcode_invalid,
            item => item is > 0 and < 10000);
    }

    public static bool ValidateAddress(Address? address)
    {
        return Validate(address,
            ErrorMessages.Address_userNotFound,
            item => item != null && item.AddressId != Guid.Empty);
    }
}