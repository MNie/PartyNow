using System;

namespace PartyNow.Mobile.Exceptions
{
    internal class LocalizationDisabled : Exception
    {
        public override string Message => "Ustawienia geolokalizacji sa wyłączone, aby wyszkać wydarzenia znajdujące się najbliżej Twojego położenia. Musisz włączyć geolokalizację w opcjach telefonu!";
    }
}
