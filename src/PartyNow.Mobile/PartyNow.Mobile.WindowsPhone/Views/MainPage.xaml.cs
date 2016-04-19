using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
using Events = PartyNow.Mobile.Views.Events;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PartyNow.Mobile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var categories = new CategoriesGetter(@"http://planer.info.pl/api/rest/categories.json").Get().Result;
            var organizers = new OrganizersGetter(@"http://planer.info.pl/api/rest/organizers.json").Get().Result;
            var places = new PlacesGetter(@"http://planer.info.pl/api/rest/places.json").Get().Result;
            foreach (var category in categories)
            {
                CategoriesCombobox.Items.Add(category);
            }
            foreach (var organizer in organizers)
            {
                OrganizersCombobox.Items.Add(organizer);
            }
            foreach (var place in places)
            {
                PlacesCombobox.Items.Add(place);
            }
            this.NavigationCacheMode = NavigationCacheMode.Required;
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
