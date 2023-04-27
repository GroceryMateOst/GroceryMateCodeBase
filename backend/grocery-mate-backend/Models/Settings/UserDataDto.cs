using System.ComponentModel.DataAnnotations;
using grocery_mate_backend.Data.DataModels.UserManagement.Address;

using User = grocery_mate_backend.Data.DataModels.UserManagement.User;

namespace grocery_mate_backend.Models.Settings;

public class UserDataDto
{
    [Required] public UserDto User { get; set; }

    [Required] public AddressDto Address { get; set; }

    public UserDataDto(User user, Address address)
    {
        User = new UserDto
        {
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            EmailAddress = user.EmailAddress,
            ResidencyDetails = user.ResidencyDetails ?? string.Empty
        };
        Address = new AddressDto
        {
            City = address.City,
            HouseNr = address.HouseNr,
            State = address.State,
            Street = address.Street,
            ZipCode = address.ZipCode
        };
    }

    public UserDataDto()
    {
        User = new UserDto();
        Address = new AddressDto();
    }
}