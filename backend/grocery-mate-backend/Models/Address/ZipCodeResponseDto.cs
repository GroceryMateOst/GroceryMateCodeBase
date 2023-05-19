namespace grocery_mate_backend.Models;

public class ZipCodeResponseDto
{
    public string City { get; set; }
    public string State { get; set; }
    
    public ZipCodeResponseDto(string city, string state)
    {
        City = city;
        State = state;
    }
    
}