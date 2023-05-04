using System.ComponentModel.DataAnnotations;
using grocery_mate_backend.Models;


namespace grocery_mate_backend.Data.DataModels.UserManagement;

public class Address
{
    public Guid AddressId { get; set; }
    [Required] public string Street { get; set; }
    [Required] public string HouseNr { get; set; }
    [Required] public int ZipCode { get; set; }
    [Required] public string City { get; set; }
    [Required] public string State { get; set; }

    public double Latitude { get; set;}
    public double Longitude { get; set;}
    public ICollection<User> Users { get; set; }

    public Address(AddressDto requestDtoAddress)
    {
        Street = requestDtoAddress.Street;
        HouseNr = requestDtoAddress.HouseNr;
        ZipCode = requestDtoAddress.ZipCode;
        City = requestDtoAddress.City;
        State = requestDtoAddress.State;
        Users = new List<User>();
    }

    public Address()
    {
        Street = string.Empty;
        HouseNr = string.Empty;
        ZipCode = -1;
        City = string.Empty;
        State = string.Empty;
        Users = new List<User>();
    }
}