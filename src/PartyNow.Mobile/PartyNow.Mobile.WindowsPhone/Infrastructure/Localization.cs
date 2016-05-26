using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using PartyNow.Mobile.Common;

namespace PartyNow.Mobile.Infrastructure
{
    internal class Localization
    {
        private readonly Geolocator _geolocator;

        public Localization()
        {
            _geolocator = Registry.Get<Geolocator>();
            _geolocator.DesiredAccuracyInMeters = 5;
        }

        private async Task<AppLocalization> GetCurrentLocalization()
        {
            var geolocalization = await _geolocator.GetGeopositionAsync(
                maximumAge: TimeSpan.FromSeconds(5),
                timeout: TimeSpan.FromSeconds(5)
                );
            return (AppLocalization) geolocalization;

        }
    }
}
