namespace grocery_mate_backend.Data.DataModels.UserManagement.Address;

public class Coordinate
{
    public Guid CoordinateId { get; set; }
    public DmsElement Latitude { get; set;}
    public DmsElement Longitude { get; set;}

    public Coordinate()
    {
        Latitude = new DmsElement();
        Longitude = new DmsElement();
    }

    public Coordinate(DmsElement latitude, DmsElement longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}

public class DmsElement
{
    public Guid DmsElementId { get; set; }
    public bool IsLatitude { get; set;}
    public bool North { get; set;}
    public bool East { get; set;}

    public int DmsDegrees { get; set;}
    public int DmsMinutes { get;set;}
    public int DmsSeconds { get; set;}

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

    public DmsElement()
    {
        IsLatitude = false;
        North = false;
        East = false;
        DmsDegrees = -1;
        DmsMinutes = -1;
        DmsSeconds = -1;
        DdElement = -1;
    }
}