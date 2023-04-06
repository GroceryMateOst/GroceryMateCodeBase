using System.ComponentModel.DataAnnotations;

namespace grocery_mate_backend.Models.Settings;

public class UserDataDto
{
    [Required] public UserDto User { get; set; }

    [Required] public AddressDto Address { get; set; }

    public UserDataDto(User user, Address address)
    {
        User = new UserDto
        {
            EmailAddress = user.EmailAddress,
            FirstName = user.FirstName,
            SecondName = user.SecondName,
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
    }
}

public class UpdateUserSettingsDto : UserDataDto
{
    [Required] public string email { get; set; }
}