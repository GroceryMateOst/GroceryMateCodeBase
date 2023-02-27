using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace grocery_mate_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ClickController : ControllerBase
{
    [HttpPost(Name = "SendClick")]
    public HttpResponseMessage SendNumber(int number)
    {
        Console.WriteLine(number);
        return new HttpResponseMessage(HttpStatusCode.OK);
    }
    
}