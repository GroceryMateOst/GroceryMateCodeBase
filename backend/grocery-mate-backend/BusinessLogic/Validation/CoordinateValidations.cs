using grocery_mate_backend.Models;

namespace grocery_mate_backend.BusinessLogic.Validation;

public class CoordinateValidations
{
    public static Coordinate CoordinateValidation(DmsElement latitude, DmsElement longitude)
    {
        DmsElement validatedLatitude;
        DmsElement validatedLongitude;

        if (latitude.DmsDegrees.Equals(0) && longitude.DmsMinutes.Equals(0) && longitude.DmsSeconds.Equals(0))
        {
            validatedLatitude = DmsValidation(true, latitude.DdElement);
            validatedLongitude = DmsValidation(false, longitude.DdElement);
        }
        else
        {
            validatedLatitude = DmsValidation(
                true,
                latitude.North,
                latitude.DmsDegrees,
                latitude.DmsMinutes,
                latitude.DmsSeconds);

            validatedLongitude = DmsValidation(
                false,
                longitude.East,
                longitude.DmsDegrees,
                longitude.DmsMinutes,
                longitude.DmsSeconds);
        }

        return new Coordinate(validatedLatitude, validatedLongitude);
    }

    public static DmsElement DmsValidation(bool latitude, bool northOrEast, int dmsDegrees, int dmsMinutes,
        int dmsSeconds)
    {
        var north = false;
        var east = false;

        if (latitude)
        {
            north = northOrEast;
        }
        else
        {
            east = northOrEast;
        }

        try
        {
            double ddElement = dmsDegrees + ((double) dmsMinutes / 60) + ((double) dmsSeconds / 3600);

            if (northOrEast == false)
            {
                ddElement *= -1;
            }

            return new DmsElement(north, east, dmsDegrees, dmsMinutes, dmsSeconds, ddElement);
        }
        catch (Exception)
        {
            throw new InvalidCoordinateException();
        }
    }

    public static DmsElement DmsValidation(bool latitude, double coordinate)
    {
        var north = false;
        var east = false;
        try
        {
            if (latitude && coordinate >= 0)
            {
                north = true;
            }
            else if (latitude && coordinate >= 0)
            {
                north = false;
            }
            else if (!latitude && coordinate >= 0)
            {
                east = true;
            }
            else if (!latitude && coordinate >= 0)
            {
                east = false;
            }

            if (coordinate < 0)
            {
                coordinate = coordinate * -1;
            }


            double minutes = (coordinate - Math.Floor(coordinate)) * 60.0;
            double seconds = (minutes - Math.Floor(minutes)) * 60.0;

            var dmsDegrees = (int) Math.Floor(coordinate);
            var dmsMinutes = (int) Math.Floor(minutes);
            var dmsSeconds = (int) Math.Floor(seconds);

            var ddElement = coordinate;

            return new DmsElement(north, east, dmsDegrees, dmsMinutes, dmsSeconds, ddElement);
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