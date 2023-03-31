using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.Extensions;

namespace grocery_mate_backend.Models.Settings;

public class UserDataResponseDto
{
    [Required] public UserDto User { get; set; }

    [Required] public AddressDto Address { get; set; }

    public UserDataResponseDto(User user, Address address)
    {
        User = new UserDto()
        {
            EmailAddress = user.EmailAddress,
            FirstName = user.FirstName,
            SecondName = user.SecondName
        };

        Address = new AddressDto()
        {
            City = address.City,
            Country = address.Country.GetDisplayName(),
            HouseNr = Convert.ToInt32(address.HouseNr),
            State = address.State,
            Street = address.Street,
            ZipCode = address.ZipCode
        };
    }
}