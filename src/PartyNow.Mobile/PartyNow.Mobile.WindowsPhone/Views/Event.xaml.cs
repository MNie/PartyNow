using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using PartyNow.DataContract.Models;


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
            if (@event?.name != null) Name.Text = @event.name;
            if (@event?.place?.name != null) Where.Text = @event.place?.name;
            if (@event?.startDate != null) StartDate.Text = @event.startDate;
            if (@event?.endDate != null) EndDate.Text = @event.endDate;
            if (@event?.organizer?.designation != null) Organizer.Text = @event.organizer?.designation;
            if (@event?.tickets?.type != null) Tickets.Text = @event.tickets?.type;
            if (@event?.descLong != null) Description.Text = @event.descLong;
            if (@event?.active != null) Active.Text = @event.active.ToString();
            if (@event?.urls?.tickets != null) WhereToBuyTickets.Text = @event.urls?.tickets;
            if (@event?.urls?.www != null) WWW.Text = @event?.urls?.www;
            if (@event?.urls?.fb != null) Facebook.Text = @event?.urls?.fb;
            DownloadImages(@event.attachments);
        }

        private async void DownloadImages(IEnumerable<Attachment> urls)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
             {
                 foreach (
                     var bmi in
                         urls.Select(attachment => new Uri(attachment.filename, UriKind.Absolute))
                             .Select(uri => new BitmapImage(uri)))
                 {
                     Attachments.Children.Add(new Image()
                     {
                         Source = bmi
                     });
                 }
             });
        }
    }
}
