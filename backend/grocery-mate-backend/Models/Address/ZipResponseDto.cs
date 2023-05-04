namespace grocery_mate_backend.Models;

public class ZipResponseDto
{
    public string City { get; set; }
    public string State { get; set; }
    
    public ZipResponseDto(string city, string state)
    {
        City = city;
        State = state;
    }
    
}