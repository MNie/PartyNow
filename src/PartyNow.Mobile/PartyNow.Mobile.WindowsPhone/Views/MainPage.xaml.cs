using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private Task<IList<Categories>> categories;
        private Task<IList<Organizers>> organizers;
        private Task<IList<Places>> places;

        public MainPage()
        {
            this.InitializeComponent();
            LocalSettings.InitUrlsInStrageSettings();
            categories = new CategoriesGetter(LocalSettings.GetUrl("categoriesUrl")).Get();
            organizers = new OrganizersGetter(LocalSettings.GetUrl("organizersUrl")).Get();
            places = new PlacesGetter(LocalSettings.GetUrl("placesUrl")).Get();
            
            this.NavigationCacheMode = NavigationCacheMode.Required;
            InitComboBoxes();
        }

        private async void InitComboBoxes()
        {
            try
            {
                foreach (var category in await categories)
                {
                    CategoriesCombobox.Items.Add(category);
                }
                foreach (var organizer in await organizers)
                {
                    OrganizersCombobox.Items.Add(organizer);
                }
                foreach (var place in await places)
                {
                    PlacesCombobox.Items.Add(place);
                }
            } 
            catch (Exception)
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
                .WhereCategoryIs(new[] {((Categories) CategoriesCombobox.SelectedValue).id})
                .WhereOrganizerIs(new[] {((Organizers) CategoriesCombobox.SelectedValue).id})
                .WherePlaceIs(new[] {((Places) CategoriesCombobox.SelectedValue).id})
                .CreateQuery();
            Frame.Navigate(typeof (Events), param);
        }
    }
}
