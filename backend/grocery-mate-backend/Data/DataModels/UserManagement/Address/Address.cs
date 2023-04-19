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

    public Coordinate? Coordinate { get; set; }
    public ICollection<User> Users { get; set; }

    public Address(Guid addressId, string street, string houseNr, int zipCode, string city, string state,
        Coordinate? coordinate, ICollection<User> users)
    {
        AddressId = addressId;
        Street = street;
        HouseNr = houseNr;
        ZipCode = zipCode;
        City = city;
        State = state;
        Coordinate = coordinate;
        Users = users;
    }

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
        Coordinate = new Coordinate();
        Users = new List<User>();
    }
}