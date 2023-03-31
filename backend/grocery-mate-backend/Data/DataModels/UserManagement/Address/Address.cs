using System.ComponentModel.DataAnnotations;

namespace grocery_mate_backend.Models;

public class Address
{
    public Guid AddressId { get; set; }
    [Required] public string Street { get; set; }
    [Required] public string HouseNr { get; set; }
    [Required] public int ZipCode { get; set; }
    [Required] public string City { get; set; }
    [Required] public string State { get; set; }
    [Required] public Countries Country { get; set; }
    public Coordinate Coordinate { get; set; }
    public ICollection<User> Users { get; set; }
}