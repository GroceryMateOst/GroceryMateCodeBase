namespace grocery_mate_backend.Models;

public class Coordinate
{
    public Guid CoordinateId { get; set; }
    public DmsElement Latitude { get; }
    public DmsElement Longitude { get; }

    public Coordinate()
    {
    }

    public Coordinate(DmsElement latitude, DmsElement longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}

public class DmsElement
{
    public bool IsLatitude { get; }
    public bool North { get; }
    public bool East { get; }

    public int DmsDegrees { get; }
    public int DmsMinutes { get; }
    public int DmsSeconds { get; }

    public double DdElement { get; }

    public DmsElement(bool north, bool east, int dmsDegrees, int dmsMinutes, int dmsSeconds, double ddElement)
    {
        North = north;
        East = east;
        DmsDegrees = dmsDegrees;
        DmsMinutes = dmsMinutes;
        DmsSeconds = dmsSeconds;
        DdElement = ddElement;
    }

    public DmsElement(bool latitude, double ddElement)
    {
        IsLatitude = latitude;
        DdElement = ddElement;
    }

    public DmsElement(bool latitude, bool northOrEast, int dmsDegrees, int dmsMinutes, int dmsSeconds)
    {
        if (latitude)
        {
            North = northOrEast;
        }
        else
        {
            East = northOrEast;
        }

        DmsDegrees = dmsDegrees;
        DmsMinutes = dmsMinutes;
        DmsSeconds = dmsSeconds;
    }
}