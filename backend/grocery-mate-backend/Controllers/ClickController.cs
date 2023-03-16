using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using grocery_mate_backend.Data;
using grocery_mate_backend.Models;
using Microsoft.EntityFrameworkCore;

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
        var newOrExisting = await _context.Clicks.FirstOrDefaultAsync(c =>c.Id == 1);
        if(newOrExisting == null){
            _context.Add(new Click{
                Id = 1,
                numberOfClicks = number,
            });
        }else{
            newOrExisting.numberOfClicks = number;
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
                Id = 1,
                numberOfClicks = 0,
            };
            return Ok(newClick);
        }
        return Ok(click);
    }

}