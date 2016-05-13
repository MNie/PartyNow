using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using PartyNow.DataContract.Models;
using PartyNow.Mobile.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace PartyNow.Mobile.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Event : Page
    {
        public Event()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += (sender, args) =>
            {
                if (!Frame.CanGoBack) return;
                args.Handled = true;
                Frame.GoBack();
            };
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
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
