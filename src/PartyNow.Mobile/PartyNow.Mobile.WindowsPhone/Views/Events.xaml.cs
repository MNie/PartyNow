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
            foreach (var x in typeof (DataContract.Models.Events).GetRuntimeProperties().Where(x => !ConstValues.EventPropertiesToExclude.Contains(x.Name)))
            {
                TypeFilterCombobox.Items?.Add(x.Name);
            }
            TypeFilterCombobox.SelectionChanged += GetValueBasedOnProperty(data);
            TypeValueFilterCombobox.SelectionChanged += FilterResultsBasedOnFilters(data);
            foreach (var @event in data.OrderBy(x => x.start))
            {
                ResultsListBox.Items?.Add(@event);
            }
        }

        private SelectionChangedEventHandler GetValueBasedOnProperty(IEnumerable<DataContract.Models.Events> data)
        {
            return (sender, args) =>
            {
                TypeValueFilterCombobox.Items?.Clear();
                foreach (var singleData in data.Select(x => x.GetType().GetRuntimeProperty(args.AddedItems.First().ToString()).GetValue(x)).Distinct())
                {
                    TypeValueFilterCombobox.Items?.Add(singleData);
                }
            };
        }

        private SelectionChangedEventHandler FilterResultsBasedOnFilters(IEnumerable<DataContract.Models.Events> data)
        {
            return (sender, args) =>
            {
                ResultsListBox.Items?.Clear();
                foreach (
                    var @event in
                        data.OrderBy(x => x.start)
                            .Where(
                                x =>
                                    x.GetType()
                                        .GetRuntimeProperty(TypeFilterCombobox?.SelectedItem?.ToString())
                                        .GetValue(x).Equals(TypeValueFilterCombobox.SelectedItem)))
                {
                    ResultsListBox.Items?.Add(@event);
                }
            };
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
