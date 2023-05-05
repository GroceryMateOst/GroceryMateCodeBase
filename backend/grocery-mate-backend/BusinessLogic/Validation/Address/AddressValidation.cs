using grocery_mate_backend.Data.DataModels.UserManagement;

namespace grocery_mate_backend.BusinessLogic.Validation;

public abstract class AddressValidation : ValidationBase
{
    public static bool ValidateZipcode(int requestState)
    {
        return Validate(requestState,
            "ValidateZipcode",
            "Zipcode is invalid",
            item => item is > 0 and < 10000);
    }
    
    public static bool ValidateAddress(Address? address)
    {
        return Validate(address,
            "ValidateAddress",
            "User with given eMail-Adr. not found",
            item => item != null && item.AddressId != Guid.Empty);
    }
}