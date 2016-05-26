using System;
using Windows.Devices.Geolocation;
using PartyNow.DataContract.Models;

namespace PartyNow.Mobile.Infrastructure
{
    public class AppLocalization : IComparable<Address>
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

        public int CompareTo(Address other)
        {
            double longitude, latitude;
            if (double.TryParse(other.lng, out longitude))
                return -1;
            if (double.TryParse(other.lat, out latitude))
                return -1;
            if (longitude.CompareTo(Longitude) == 0 && latitude.CompareTo(Latitude) == 0)
                return 0;
            return 1;
        }
    }
}
