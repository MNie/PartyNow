using System;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml.Navigation;
using PartyNow.Mobile.ViewModels;


namespace PartyNow.Mobile.Views
{
    public sealed partial class Event
    {
        public Event()
        {
            InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtonsOnBackPressed;
        }

        private void HardwareButtonsOnBackPressed(object sender, BackPressedEventArgs e)
        {
            if (!Frame.CanGoBack) return;
            e.Handled = true;
            Frame.GoBack();
            HardwareButtons.BackPressed -= HardwareButtonsOnBackPressed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var @event = e.Parameter as DataContract.Models.Events;
            this.DataContext = new EventModel(@event);
        }
    }
}
