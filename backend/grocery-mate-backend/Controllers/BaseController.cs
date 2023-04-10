using grocery_mate_backend.Controllers.Services;
using grocery_mate_backend.Data;
using grocery_mate_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using grocery_mate_backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using User = grocery_mate_backend.Data.DataModels.UserManagement.User;

namespace grocery_mate_backend.Controllers;

[ApiController]
[Route("api/v0/base")]
// Base class from which all further REST API controllers derive
public class BaseController : ControllerBase
{
    public GroceryContext _context;
    public UserManager<IdentityUser> _userManager;
    public JwtService _jwtService;

    public AuthenticationService _authenticationService;
    public AddressService _addressService;
    public UserService _userService;

    public BaseController(GroceryContext context, UserManager<IdentityUser> userManager, JwtService jwtService)
    {
        _context = context;
        _userManager = userManager;
        _jwtService = jwtService;

        _authenticationService = new AuthenticationService(userManager);
        _addressService = new AddressService(context);
        _userService = new UserService(context);
    }

    public BaseController(GroceryContext context)
    {
        _context = context;
    }

    public BaseController()
    {
    }
}