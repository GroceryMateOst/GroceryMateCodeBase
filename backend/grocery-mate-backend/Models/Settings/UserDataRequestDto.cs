using grocery_mate_backend.Sandbox;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace grocery_mate_backend.Models.Settings;

public class UserDataRequestDto
{
    [BindRequired] public string email { get; set; }

    public UserDataRequestDto(string email)
    {
        this.email = email;
    }

    public UserDataRequestDto()
    {
        this.email = Symbols.Empty;
    }
}