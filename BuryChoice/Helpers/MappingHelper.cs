using GeoUK;
using GeoUK.Coordinates;
using GeoUK.Ellipsoids;
using GeoUK.Projections;

namespace BuryChoice.Helpers
{
    public class MappingHelper
    {
        public static LatitudeLongitude? ConvertToLatLon(string easting, string northing)
        {
            if (string.IsNullOrWhiteSpace(easting) || string.IsNullOrWhiteSpace(northing))
            {
                return null;
            }

            bool canParseEasting = double.TryParse(easting, out double Easting);
            bool canParseNorthing = double.TryParse(northing, out double Northing);

            if (!canParseEasting || !canParseNorthing)
            {
                return null;
            }

            Cartesian cartesian = GeoUK.Convert.ToCartesian(new Airy1830(),
                new BritishNationalGrid(),
                new EastingNorthing(Easting, Northing));

            Cartesian wgsCartesian = Transform.Osgb36ToEtrs89(cartesian);

            return GeoUK.Convert.ToLatitudeLongitude(new Wgs84(), wgsCartesian);
        }

        public static double MilesToMeters(double miles)
        {
            const double metersPerMile = 1609.344;
            return miles * metersPerMile;
        }

        public static double MilesToKilometers(double miles)
        {
            var meters = MilesToMeters(miles);
            return meters / 1000;
        }
    }
}
