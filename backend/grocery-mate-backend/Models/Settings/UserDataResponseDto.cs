using System.ComponentModel.DataAnnotations;

namespace grocery_mate_backend.Models.Settings;

public class UserDataResponseDto
{
    [Required] public UserDto UserDto { get; set; }
    
    [Required] public AddressDto AddressDto { get; set; }
}
