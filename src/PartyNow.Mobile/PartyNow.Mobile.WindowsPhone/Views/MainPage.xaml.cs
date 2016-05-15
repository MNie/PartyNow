using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using PartyNow.DataContract.Common;
using PartyNow.DataContract.Models;
using PartyNow.DataContract.Service;
using PartyNow.Mobile.Common;
using Events = PartyNow.Mobile.Views.Events;

namespace PartyNow.Mobile
{
    public sealed partial class MainPage
    {
        private readonly IBaseGetter<Categories> _categoriesGetter;
        private readonly IBaseGetter<Organizers> _organizersGetter;
        private readonly IBaseGetter<Places> _placesGetter;
        private IList<Categories> _categories;
        private IList<Organizers> _organizers;
        private IList<Places> _places;

        public MainPage()
        {
            InitializeComponent();
            LocalSettings.InitUrlsInStrageSettings();
            _categoriesGetter = Registry.Get<IBaseGetter<Categories>>();
            _organizersGetter = Registry.Get<IBaseGetter<Organizers>>();
            _placesGetter = Registry.Get<IBaseGetter<Places>>();

            NavigationCacheMode = NavigationCacheMode.Required;

            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            var msgDialog = new MessageDialog(ConstValues.Communicates.ExitApp);
            msgDialog.Commands.Add(new UICommand(ConstValues.Communicates.Yes, BackButtonHandler));
            msgDialog.Commands.Add(new UICommand(ConstValues.Communicates.No, BackButtonHandler));
            await msgDialog.ShowAsync();
        }

        private static void BackButtonHandler(IUICommand command)
        {
            var label = command.Label;
            switch (label)
            {
                case ConstValues.Communicates.Yes:
                    {
                        Application.Current.Exit();
                        break;
                    }
                case ConstValues.Communicates.No:
                    {
                        break;
                    }
            }
        }

        private async Task InitComboBoxes()
        {

            _categories = await Task.Run(() => _categoriesGetter.Get());
            _organizers = await Task.Run(() => _organizersGetter.Get());
            _places = await Task.Run(() => _placesGetter.Get());
            try
            {
                foreach (var category in _categories.Where(x => x.root_category == null).Concat(new [] {new Categories() {id = null, name = ConstValues.AllOptions} }))
                {
                    CategoriesCombobox.Items?.Add(category);
                }
                foreach (var organizer in _organizers.Concat(new[] { new Organizers()  { id = null, designation = ConstValues.AllOptions } }))
                {
                    OrganizersCombobox.Items?.Add(organizer);
                }
                foreach (var place in _places.Where(x => x.subname == null).Concat(new[] { new Places() { id = null, name = ConstValues.AllOptions } }))
                {
                    PlacesCombobox.Items?.Add(place);
                }
            }
            catch (Exception e)
            {
                var msg = new MessageDialog("Please check Your internet connection!");
                await msg.ShowAsync();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var param = new QueryBuilder()
                .WhereCategoryIs(new[] { ((Categories)CategoriesCombobox.SelectedValue)?.id })
                .WhereOrganizerIs(new [] { ((Organizers) OrganizersCombobox.SelectedValue)?.id})
                .WherePlaceIs(new[] { ((Places)PlacesCombobox.SelectedValue)?.id })
                .WhereStartDateIs(DateTime.Now.ToString("yyyy-MM-dd"))
                .CreateQuery();
            Frame.Navigate(typeof (Events), param);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await InitComboBoxes();
        }
    }
}
