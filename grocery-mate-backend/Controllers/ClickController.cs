using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using grocery_mate_backend.Data;
using grocery_mate_backend.Models;

namespace grocery_mate_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ClickController : ControllerBase
{
    private readonly ClickDbContext _context;
    private readonly ILogger<ClickController> _logger;

    public ClickController(ILogger<ClickController> logger, ClickDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost(Name = "SendClick")]
    public async Task<IActionResult> SendNumber(int number)
    {
        Console.WriteLine(number);
        var newOrExisting = _context.Clicks.Find(1);
        if(newOrExisting == null){
        _context.Add(new Click{
            id = 1,
            click = number,
        });
        }else{
            newOrExisting.click = number;
        }
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet(Name = "GetClicks")]
    public IActionResult GetNumber()
    {
        var click = _context.Clicks.Find(1);
        if(click == null){
            var newClick = new Click{
                id = 1,
                click = 0,
            };
            return Ok(newClick);
        }
        return Ok(click);
    }

}