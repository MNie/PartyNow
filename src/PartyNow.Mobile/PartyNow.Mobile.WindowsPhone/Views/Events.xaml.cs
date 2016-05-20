using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using PartyNow.DataContract.Service;
using PartyNow.Mobile.Common;

namespace PartyNow.Mobile.Views
{
    public sealed partial class Events
    {
        private readonly IRichGetter<DataContract.Models.Events> _eventsGetter;
        public Events()
        {
            InitializeComponent();
            _eventsGetter = Registry.Get<IRichGetter<DataContract.Models.Events>>();
            HardwareButtons.BackPressed += HardwareButtonsOnBackPressed;
        }

        private void HardwareButtonsOnBackPressed(object sender, BackPressedEventArgs e)
        {
            if (!Frame.CanGoBack) return;
            e.Handled = true;
            HardwareButtons.BackPressed -= HardwareButtonsOnBackPressed;
            Frame.GoBack();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var query = e.Parameter as string;
            var data = await GetData(query);
            TypeFilterCombobox.Items?.Add(typeof(DataContract.Models.Events).GetRuntimeProperties());
            TypeFilterCombobox.SelectionChanged += (sender, args) =>
            {
                TypeValueFilterCombobox.Items?.Add(
                    data.Select(x => x.GetType().GetRuntimeProperty(args.AddedItems.First().ToString()).GetValue(x)));
            };
            foreach (var @event in data.OrderBy(x => x.startDate))
            {
                ResultsListBox.Items?.Add(@event);
            }
        }

        private async Task<IList<DataContract.Models.Events>> GetData(string parameter)
        {
            return await _eventsGetter.GetBasedOnQuery(parameter);
        }

        private void ResultsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var @event = e.AddedItems.FirstOrDefault();
            HardwareButtons.BackPressed -= HardwareButtonsOnBackPressed;
            Frame.Navigate(typeof (Event), @event);
        }
    }
}
