using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
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
        public CategoriesGetter categoriesGetter;
        public OrganizersGetter organizersGetter;
        public PlacesGetter placesGetter;
        public IList<Categories> categories;
        public IList<Organizers> organizers;
        public IList<Places> places;

        public MainPage()
        {
            this.InitializeComponent();
            LocalSettings.InitUrlsInStrageSettings();
            categoriesGetter = new CategoriesGetter(LocalSettings.GetUrl("categoriesUrl"));
            organizersGetter = new OrganizersGetter(LocalSettings.GetUrl("organizersUrl"));
            placesGetter = new PlacesGetter(LocalSettings.GetUrl("placesUrl"));

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private async Task InitComboBoxes()
        {

            categories = await Task.Run(() => categoriesGetter.Get());
            organizers = await Task.Run(() => organizersGetter.Get());
            places = await Task.Run(() => placesGetter.Get());
            try
            {
                foreach (var category in categories.Where(x => x.root_category == null).Concat(new [] {new Categories() {id = null, name = "Wszystkie"} }))
                {
                    CategoriesCombobox.Items.Add(category);
                }
                foreach (var organizer in organizers.Concat(new[] { new Organizers()  { id = null, designation = "Wszystkie" } }))
                {
                    OrganizersCombobox.Items.Add(organizer);
                }
                foreach (var place in places.Where(x => x.subname == null).Concat(new[] { new Places() { id = null, name = "Wszystkie" } }))
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
                .WhereCategoryIs(new[] { ((Categories)CategoriesCombobox.SelectedValue).id })
                .WhereOrganizerIs(new[] { ((Organizers)OrganizersCombobox.SelectedValue).id })
                .WherePlaceIs(new[] { ((Places)PlacesCombobox.SelectedValue).id })
                .CreateQuery();
            Frame.Navigate(typeof(Events), param);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await InitComboBoxes();
        }
    }
}
