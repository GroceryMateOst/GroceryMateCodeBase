using System.ComponentModel.DataAnnotations;

namespace grocery_mate_backend.Models;

public class AddressDto
{
    [Required]
    [StringLength(maximumLength: 50, ErrorMessage = "Street is too long")]
    public string Street { get; set; }

    [StringLength(maximumLength: 5, ErrorMessage = "House-Number is too long")]
    [Required]
    public int HouseNr { get; set; }

    [Required]
    [StringLength(maximumLength: 8, ErrorMessage = "Zip-Code is too long")]
    public int ZipCode { get; set; }

    [Required]
    [StringLength(maximumLength: 20, ErrorMessage = "City-Name is too long")]
    public string City { get; set; }

    [Required]
    [StringLength(maximumLength: 50, ErrorMessage = "State-Name is too long")]
    public string State { get; set; }

    [Required]
    [StringLength(maximumLength: 50, ErrorMessage = "Country-Name is too long")]
    public string Country { get; set; }
}

public class UserInfo_AddressDto : AddressDto
{
    [Required]
    [StringLength(maximumLength: 16, ErrorMessage = "Latitude is too long")]
    public double CoordinateLatitude  { get; set; }
    
    [Required]
    [StringLength(maximumLength: 20, ErrorMessage = "Longitude is too long")]
    public double CoordinateLongitude { get; set; }
}

