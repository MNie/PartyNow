using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using PartyNow.DataContract.Models;
using PartyNow.Mobile.Exceptions;

namespace PartyNow.Mobile.Infrastructure
{
    public interface ILocalization
    {
        Task<int?[]> GetPlacesIdBasedOnCurrentLocalization(IEnumerable<Places> places);
    }

    internal class Localization : ILocalization
    {
        private readonly Geolocator _geolocator;
        private const double MaxDistanceToNearbyEventLocalization = 1.5;

        public Localization(Geolocator geolocator)
        {
            _geolocator = geolocator;
            _geolocator.DesiredAccuracyInMeters = 5;
        }

        private async Task<AppLocalization> GetCurrentLocalization()
        {
            if (_geolocator.LocationStatus == PositionStatus.Disabled)
            {
                throw new LocalizationDisabled();
            }
            var geolocalization = await _geolocator.GetGeopositionAsync(
                maximumAge: TimeSpan.FromSeconds(5),
                timeout: TimeSpan.FromSeconds(5)
                );
            return (AppLocalization) geolocalization;
        }

        public async Task<int?[]> GetPlacesIdBasedOnCurrentLocalization(IEnumerable<Places> places)
        {
            var currentLocalization = await GetCurrentLocalization();
            return places.Where(place => ((AppLocalization) place).Distance(currentLocalization) < MaxDistanceToNearbyEventLocalization).Select(x => x.id).ToArray();
        }
    }
}
