using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace grocery_mate_backend.Models.Settings;

public class UserDataRequestDto
{
    [BindRequired] public string email { get; set; }
}
