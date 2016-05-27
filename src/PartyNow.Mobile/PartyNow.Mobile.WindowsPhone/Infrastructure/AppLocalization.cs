using System;
using System.Globalization;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using PartyNow.DataContract.Models;

namespace PartyNow.Mobile.Infrastructure
{
    public class AppLocalization
    {
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }
        public double Altitude { get; private set; }

        public static explicit operator AppLocalization(Geoposition geoposition)
        {
            return new AppLocalization()
            {
                Longitude = geoposition.Coordinate.Point.Position.Longitude,
                Latitude = geoposition.Coordinate.Point.Position.Latitude,
                Altitude = geoposition.Coordinate.Point.Position.Altitude
            };
        }

        public static explicit operator AppLocalization(Places place)
        {
            double longitude, latitude;
            double.TryParse(place.address.lng, NumberStyles.Float, CultureInfo.InvariantCulture, out longitude);
            double.TryParse(place.address.lat, NumberStyles.Float, CultureInfo.InvariantCulture, out latitude);
            return new AppLocalization
            {
                Longitude = longitude,
                Latitude = latitude,
                Altitude = 0
            };
        }
    }

    public static class AppLocalizationExtension
    {
        private const double Radius = 6378.16;

        private static double Radians(double x)
        {
            return x*Math.PI/180;
        }
        public static double Distance(this AppLocalization first, AppLocalization second)
        {
            var longitudeDistance = Radians(second.Longitude - first.Longitude);
            var latitudeDistance = Radians(second.Latitude - first.Latitude);

            var a = Math.Pow(Math.Sin(latitudeDistance/2), 2) +
                    Math.Cos(Radians(first.Latitude))*Math.Cos(Radians(second.Latitude))*
                    Math.Pow(Math.Sin(longitudeDistance/2), 2);
            var angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return angle * Radius;
        }
    }
}
