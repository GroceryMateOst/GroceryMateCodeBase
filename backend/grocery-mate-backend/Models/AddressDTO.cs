using System.ComponentModel.DataAnnotations;
using grocery_mate_backend.Sandbox;

namespace grocery_mate_backend.Models;

public class AddressDto
{
    [Required]
    [StringLength(maximumLength: 50, ErrorMessage = "Street is too long")]
    public string Street { get; set; }

    [StringLength(maximumLength: 5, ErrorMessage = "House-Number is too long")]
    [Required]
    public string HouseNr { get; set; }

    [Required] public int ZipCode { get; set; }

    [Required]
    [StringLength(maximumLength: 20, ErrorMessage = "City-Name is too long")]
    public string City { get; set; }

    [Required]
    [StringLength(maximumLength: 50, ErrorMessage = "State-Name is too long")]
    public string State { get; set; }

    public AddressDto(string street, string houseNr, int zipCode, string city, string state)
    {
        Street = street;
        HouseNr = houseNr;
        ZipCode = zipCode;
        City = city;
        State = state;
    }

    public AddressDto()
    {
        Street = Symbols.Empty;
        HouseNr = Symbols.Empty;
        ZipCode = -1;
        City = Symbols.Empty;
        State = Symbols.Empty;
    }
}

public class UserInfo_AddressDto : AddressDto
{
    [Required]
    [StringLength(maximumLength: 16, ErrorMessage = "Latitude is too long")]
    public double CoordinateLatitude { get; set; }

    [Required]
    [StringLength(maximumLength: 20, ErrorMessage = "Longitude is too long")]
    public double CoordinateLongitude { get; set; }
}