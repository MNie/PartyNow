using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using PartyNow.DataContract.Service;
using PartyNow.Mobile.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace PartyNow.Mobile.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Events : Page
    {
        public Events()
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
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var query = e.Parameter as string;
            var data = await GetData(query);
            foreach (var @event in data.OrderBy(x => DateTime.Parse(x.startDate)))
            {
                ResultsListBox.Items.Add(@event);
            }

        }

        private static async Task<IList<DataContract.Models.Events>> GetData(string parameter)
        {
            return await new EventsGetter(LocalSettings.GetUrl(ConstValues.EventsUrl)).GetBasedOnQuery(parameter);
        }

        private void ResultsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var @event = e.AddedItems.FirstOrDefault();
            Frame.Navigate(typeof (Event), @event);
        }
    }
}
