namespace grocery_mate_backend.Models;

public class Coordinates
{
    public DmsElement Latitude { get; }
    public DmsElement Longitude { get; }

    public Coordinates(DmsElement latitude, DmsElement longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}

public class DmsElement
{
    public bool North { get; }
    public bool East { get; }

    public int DmsDegrees { get; }
    public int DmsMinutes { get; }
    public int DmsSeconds { get; }

    public double DdElement { get; }

    //DMS-Constructor
    public DmsElement(bool latitude, bool northOrEast, int dmsDegrees, int dmsMinutes, int dmsSeconds)
    {
        DmsDegrees = dmsDegrees;
        DmsMinutes = dmsMinutes;
        DmsSeconds = dmsSeconds;

        if (latitude)
        {
            North = northOrEast;
        }
        else
        {
            East = northOrEast;
        }

        try
        {
            double ddElement = dmsDegrees + ((double) dmsMinutes / 60) + ((double) dmsSeconds / 3600);

            if (northOrEast == false)
            {
                ddElement *= -1;
            }

            DdElement = ddElement;
        }
        catch (Exception)
        {
            throw new InvalidCoordinateException();
        }
    }

    // DD-Constructor
    public DmsElement(bool latitude, double coordinate)
    {
        try
        {
            if (latitude && coordinate >= 0)
            {
                North = true;
            }
            else if (latitude && coordinate >= 0)
            {
                North = false;
            }
            else if (!latitude && coordinate >= 0)
            {
                East = true;
            }
            else if (!latitude && coordinate >= 0)
            {
                East = false;
            }

            if (coordinate < 0)
            {
                coordinate = coordinate * -1;
            }


            double minutes = (coordinate - Math.Floor(coordinate)) * 60.0;
            double seconds = (minutes - Math.Floor(minutes)) * 60.0;

            DmsDegrees = (int) Math.Floor(coordinate);
            DmsMinutes = (int) Math.Floor(minutes);
            DmsSeconds = (int) Math.Floor(seconds);

            DdElement = coordinate;
        }
        catch (Exception)
        {
            throw new InvalidCoordinateException();
        }
    }
}

public class InvalidCoordinateException : Exception
{
    public InvalidCoordinateException() : base("Invalid Coordinates Address")
    {
    }
}