using System.ComponentModel.DataAnnotations;
using grocery_mate_backend.Models;
using grocery_mate_backend.Sandbox;

namespace grocery_mate_backend.Data.DataModels.UserManagement.Address;

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
        Street = Symbols.Empty;
        HouseNr = Symbols.Empty;
        ZipCode = -1;
        City = Symbols.Empty;
        State = Symbols.Empty;
        Users = new List<User>();
    }
}