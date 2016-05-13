using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using PartyNow.DataContract.Common;
using PartyNow.DataContract.Models;
using PartyNow.DataContract.Service;
using PartyNow.Mobile.Common;
using Events = PartyNow.Mobile.Views.Events;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PartyNow.Mobile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly CategoriesGetter _categoriesGetter;
        private readonly OrganizersGetter _organizersGetter;
        private readonly PlacesGetter _placesGetter;
        private IList<Categories> _categories;
        private IList<Organizers> _organizers;
        private IList<Places> _places;

        public MainPage()
        {
            this.InitializeComponent();
            LocalSettings.InitUrlsInStrageSettings();
            _categoriesGetter = new CategoriesGetter(LocalSettings.GetUrl(ConstValues.CategoriesUrl));
            _organizersGetter = new OrganizersGetter(LocalSettings.GetUrl(ConstValues.OrganizersUrl));
            _placesGetter = new PlacesGetter(LocalSettings.GetUrl(ConstValues.PlacesUrl));

            this.NavigationCacheMode = NavigationCacheMode.Required;

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
                    CategoriesCombobox.Items.Add(category);
                }
                foreach (var organizer in _organizers.Concat(new[] { new Organizers()  { id = null, designation = ConstValues.AllOptions } }))
                {
                    OrganizersCombobox.Items.Add(organizer);
                }
                foreach (var place in _places.Where(x => x.subname == null).Concat(new[] { new Places() { id = null, name = ConstValues.AllOptions } }))
                {
                    PlacesCombobox.Items.Add(place);
                }
            }
            catch (Exception e)
            {
                var msg = new MessageDialog("Please check Your internet connection!");
                await msg.ShowAsync();
            }
        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
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
