using System.ComponentModel.DataAnnotations;
using grocery_mate_backend.Data.DataModels.UserManagement;

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
        if (address is not null)
        {
            Address = new AddressDto
            {
                City = address.City,
                HouseNr = address.HouseNr,
                State = address.State,
                Street = address.Street,
                ZipCode = address.ZipCode,
                Longitude = address.Longitude,
                Latitude = address.Latitude
            };
        }
        else
        {
            Address = null;
        }
    }

    public UserDataDto()
    {
        User = new UserDto();
        Address = new AddressDto();
    }
}