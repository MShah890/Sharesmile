using ShareSmile.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ShareSmile.Models;
using System.Collections.ObjectModel;
using Windows.Devices.Geolocation;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ShareSmile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DriverHome : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private ObservableCollection<Transaction_Table> requests { get; set; }

        public DriverHome()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            List<Transaction_Table> list = Transaction_Table.Get_Transactions("driver");

            requests = new ObservableCollection<Transaction_Table>(list);

            lb_Requests.ItemsSource = requests;

        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        //private async void Accept_Request_Click(object sender, RoutedEventArgs e)
        //{
        //    Geolocator geolocator = new Geolocator();
        //    Geoposition coordinate = await geolocator.GetGeopositionAsync();

        //    string latitude = coordinate.Coordinate.Latitude.ToString();
        //    string longitude = coordinate.Coordinate.Longitude.ToString();
        //    int lat_dec_index = latitude.IndexOf(".");
        //    int lon_dec_index = longitude.IndexOf(".");
        //    selected_tt.L_Driver = latitude.Substring(0,lat_dec_index+4) + "," + longitude.Substring(0,lon_dec_index+4);

        //    selected_tt.Driver_Id = driver.Driver_Id;
        //    selected_tt.Status = "Active";
        //    selected_tt.Update();
        //    App.transaction_table = selected_tt;
        //    App.volunteer = Volunteer.Get_VolunteerById(selected_tt.Volunteer_Id??default(int));
        //    App.user = User.Get_UserById(selected_tt.User_Id ?? default(int));
        //    this.Frame.Navigate(typeof(DriverSession));
        //}

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EditDriverProfile));
        }

        private void lb_Requests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.transaction_table = (Transaction_Table)lb_Requests.SelectedItem;
            this.Frame.Navigate(typeof(DriverHomeDetails));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            App.driver = null;
            App.volunteer = null;
            App.user = null;
            App.transaction_table = null;

            this.Frame.Navigate(typeof(Login));
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DriverHome));
        }
    }
}
