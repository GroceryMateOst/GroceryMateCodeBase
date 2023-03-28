using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.Models;

namespace grocery_mate_backend.BusinessLogic.Calculation;

public class DistanceCalculation
{
    /**
     * Great-circle distance between two points – that is, the shortest
     * distance between the points (ignoring any hills they fly over, of course!).
     * https://www.movable-type.co.uk/scripts/latlong.html
     */
    public static double Distance(Coordinate coordinateOne, Coordinate coordinateTwo)
    {
        if (coordinateOne.Latitude.DdElement == 0)
        {
            // coordinateOne.Latitude = CoordinateValidations.DmsValidation()
        }
        
        var earthRadius = 6371e3; // [metres]
        var lat1 = coordinateOne.Latitude.DdElement * Math.PI / 180; // [rad]
        var lat2 = coordinateTwo.Latitude.DdElement * Math.PI / 180; // [rad]
        var deltaLatitude = (coordinateTwo.Latitude.DdElement - coordinateOne.Latitude.DdElement) * Math.PI / 180;
        var deltaLongitude = (coordinateTwo.Latitude.DdElement - coordinateOne.Latitude.DdElement) * Math.PI / 180;

        var a = 
            Math.Sin((deltaLatitude / 2)) * Math.Sin(deltaLatitude / 2) + 
            Math.Cos(lat1) * Math.Cos(lat2) *
            Math.Sin(deltaLongitude / 2) * Math.Sin(deltaLongitude / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return earthRadius * c; // [metres]
    }
}