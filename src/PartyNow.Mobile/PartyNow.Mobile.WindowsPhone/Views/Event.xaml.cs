using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using PartyNow.DataContract.Models;
using PartyNow.Mobile.ViewModels;


namespace PartyNow.Mobile.Views
{
    public sealed partial class Event
    {
        public Event()
        {
            InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtonsOnBackPressed();
        }

        private EventHandler<BackPressedEventArgs> HardwareButtonsOnBackPressed()
        {
            return (sender, args) =>
            {
                if (!Frame.CanGoBack) return;
                args.Handled = true;
                Frame.GoBack();
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var @event = e.Parameter as DataContract.Models.Events;
            this.DataContext = new EventModel(@event);
        }
    }
}
