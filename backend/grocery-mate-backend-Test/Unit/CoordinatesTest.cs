using grocery_mate_backend.Models;
using NUnit.Framework;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace grocery_mate_backend_Test.Unit;

public class CoordinatesTest
{
    private Coordinates _coordinates;
    

    [Xunit.Theory]
    [InlineData(47.47605059989054, 9.09940924521073, 47, 28, 33, 9, 5, 57, true, true)]
    [InlineData(49.284364, -123.109054, 49, 17, 3, 123, 6, 32, true, false)]
    [InlineData(-23.60934389859517, -46.59599408304574, 23, 36, 33, 46, 35, 45, false, false)]
    [InlineData(-28.772518305803892, 132.9821266254198, 28, 46, 21, 132, 58, 55, false, true)]
    private void Constructor_DD(
        double ddLatitude, double ddLongitude,
        int dmdLatitudeDeg, int dmdLatitudeMin, int dmdLatitudeSec,
        int dmdLongDeg, int dmdLongMin, int dmdLongSec,
        bool dmdLatNorth, bool dmdLongEast
    )
    {
        _coordinates = new Coordinates(
            new DmsElement(true, ddLatitude),
            new DmsElement(false, ddLongitude));

        var latNorth = _coordinates.Latitude.North;
        var latDeg = _coordinates.Latitude.DmsDegrees;
        var latMin = _coordinates.Latitude.DmsMinutes;
        var latSec = _coordinates.Latitude.DmsSeconds;

        var longEast = _coordinates.Longitude.East;
        var longDeg = _coordinates.Longitude.DmsDegrees;
        var longMin = _coordinates.Longitude.DmsMinutes;
        var longSec = _coordinates.Longitude.DmsSeconds;

        Assert.Multiple(() =>
        {
            Assert.That(dmdLatNorth, Is.EqualTo(latNorth));
            Assert.That(latDeg, Is.EqualTo(dmdLatitudeDeg));
            Assert.That(latMin, Is.EqualTo(dmdLatitudeMin));
            Assert.That(latSec, Is.EqualTo(dmdLatitudeSec));

            Assert.That(longEast, Is.EqualTo(dmdLongEast));
            Assert.That(longDeg, Is.EqualTo(dmdLongDeg));
            Assert.That(longMin, Is.EqualTo(dmdLongMin));
            Assert.That(longSec, Is.EqualTo(dmdLongSec));
        });
    }

    [Xunit.Theory]
    [InlineData(47.475833333333334, 9.0991666666666671, 47, 28, 33, 9, 5, 57, true, true)]
    [InlineData(49.284166666666664, -123.10888888888888, 49, 17, 3, 123, 6, 32, true, false)]
    [InlineData(-23.609166666666667, -46.595833333333339, 23, 36, 33, 46, 35, 45, false, false)]
    [InlineData(-28.772499999999997, 132.98194444444445, 28, 46, 21, 132, 58, 55, false, true)]
    private void Constructor_DMS(
        double ddLatitude, double ddLongitude,
        int dmdLatitudeDeg, int dmdLatitudeMin, int dmdLatitudeSec,
        int dmdLongDeg, int dmdLongMin, int dmdLongSec,
        bool north, bool east
    )
    {
        _coordinates = new Coordinates(
            new DmsElement(true, north, dmdLatitudeDeg, dmdLatitudeMin, dmdLatitudeSec),
            new DmsElement(false, east, dmdLongDeg, dmdLongMin, dmdLongSec));

        var latNorth = _coordinates.Latitude.North;
        var lat = _coordinates.Latitude.DdElement;

        var longEast = _coordinates.Longitude.East;
        var lon = _coordinates.Longitude.DdElement;

        Assert.Multiple(() =>
        {
            Assert.That(north, Is.EqualTo(latNorth));
            Assert.That(lat, Is.EqualTo(ddLatitude));

            Assert.That(east, Is.EqualTo(longEast));
            Assert.That(lon, Is.EqualTo(ddLongitude));
        });
    }
}