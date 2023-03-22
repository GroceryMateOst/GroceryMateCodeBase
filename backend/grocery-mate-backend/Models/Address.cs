namespace grocery_mate_backend.Models;

public class Address
{
    public string Street { get; }
    public string HouseNr { get; }
    public int ZipCode { get; }
    public string City { get; }
    public string State { get; }
    public Countries Country { get; }
    public Coordinates Coordinates { get; }

    public Address(string street, string houseNr, int zipCode, string city, string state, Countries country,
        Coordinates coordinates)
    {
        Street = street;
        HouseNr = houseNr;
        ZipCode = zipCode;
        City = city;
        State = state;
        Country = Countries.AE;
        Coordinates = coordinates;
    }
}