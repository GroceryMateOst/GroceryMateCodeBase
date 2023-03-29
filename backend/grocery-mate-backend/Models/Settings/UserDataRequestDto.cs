using System.ComponentModel.DataAnnotations;

namespace grocery_mate_backend.Models.Settings;

public class UserDataRequestDto
{
    [Required] public string email { get; set; }
}
