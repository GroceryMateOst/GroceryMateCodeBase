using grocery_mate_backend.BusinessLogic.Calculation;
using grocery_mate_backend.Models;
using Xunit;

namespace grocery_mate_backend_Test.Unit;

public class DistanceCalculationTest
{
    private DistanceCalculation _distanceCalculation;
    //
    // [Theory]
    // // [InlineData(50.0663889, -5.714722222222222, 58.6438889, -3.0700000000000003, 968900)]
    // [InlineData(47.223039, 8.815932, 47.422632, 9.367329, 47110)]
    // public void CoordinateDistanceCalculation_DD_true(
    //     double ddOneLatitude, double ddOneLongitude,
    //     double ddTwoLatitude, double ddTwoLongitude,
    //     double distance)
    // {
    //     var calculatedDistance = DistanceCalculation.Distance(
    //         new Coordinate(
    //             new DmsElement(true, ddOneLatitude),
    //             new DmsElement(false, ddOneLongitude)
    //         ),
    //         new Coordinate(
    //             new DmsElement(true, ddTwoLatitude),
    //             new DmsElement(false, ddTwoLongitude)));
    //
    //     Assert.Equal(distance, calculatedDistance);
    // }
    //
    // [Theory]
    // [InlineData(true, false, 50, 03, 59, false, false, 5, 42, 53, true, false, 58, 38, 38, false, false, 3, 04, 12, 968900)]
    // public void CoordinateDistanceCalculation_DMS_true(
    //     bool pOneLatNorth, bool pOneLatEast, int pOneDmsLatDegrees, int pOneDmsLatMinutes, int pOneDmsLatSeconds,
    //     bool pOneLonNorth, bool pOneLonEast, int pOneDmsLonDegrees, int pOneDmsLonMinutes, int pOneDmsLonSeconds, 
    //     bool pTwoLatNorth, bool pTwoLatEast, int pTwoDmsLatDegrees, int pTwoDmsLatMinutes, int pTwoDmsLatSeconds,
    //     bool pTwoLonNorth, bool pTwoLonEast, int pTwoDmsLonDegrees, int pTwoDmsLonMinutes, int pTwoDmsLonSeconds, 
    //     int distance
    // )
    // {
    //     var calculatedDistance = DistanceCalculation.Distance(
    //         new Coordinate(
    //             new DmsElement( pOneLatNorth, pOneLatEast, pOneDmsLatDegrees, pOneDmsLatMinutes, pOneDmsLatSeconds),
    //             new DmsElement( pOneLonNorth,  pOneLonEast,  pOneDmsLonDegrees,  pOneDmsLonMinutes,  pOneDmsLonSeconds)
    //         ),
    //         new Coordinate(
    //             new DmsElement(pTwoLatNorth, pTwoLatEast, pTwoDmsLatDegrees,  pTwoDmsLatMinutes,  pTwoDmsLatSeconds),
    //             new DmsElement(pTwoLonNorth,  pTwoLonEast,  pTwoDmsLonDegrees,  pTwoDmsLonMinutes,  pTwoDmsLonSeconds)));
    //
    //     Assert.Equal(distance, calculatedDistance);
    // }
}