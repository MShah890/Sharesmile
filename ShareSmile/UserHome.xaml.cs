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
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Storage;
using ShareSmile.HelperClasses;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ShareSmile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserHome : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public Geolocator geolocator;
        Geoposition coordinate;
              //ApplicationDataCompositeValue composite;
        public UserHome()
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
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            //var localsettings=ApplicationData.Current.LocalSettings;
            //composite = (ApplicationDataCompositeValue)localsettings.Values["User_Session"];
            //ConvertToComposite();

            geolocator = new Geolocator();

            coordinate = await geolocator.GetGeopositionAsync();

            // Create a MapIcon.
            MapIcon mapIcon1 = new MapIcon();
            mapIcon1.Location = new Geopoint(new BasicGeoposition { Latitude = coordinate.Coordinate.Latitude + .001, Longitude = coordinate.Coordinate.Longitude +.002 });
            mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon1.Title = "Volunteer";
            mapIcon1.ZIndex = 0;

            MapIcon mapIcon2 = new MapIcon();
            mapIcon2.Location = new Geopoint(new BasicGeoposition { Latitude = coordinate.Coordinate.Latitude + .002, Longitude = coordinate.Coordinate.Longitude + .005 });
            mapIcon2.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon2.Title = "Volunteer";
            mapIcon2.ZIndex = 0;

            MapIcon mapIcon3 = new MapIcon();
            mapIcon3.Location = new Geopoint(new BasicGeoposition { Latitude = coordinate.Coordinate.Latitude + .006, Longitude = coordinate.Coordinate.Longitude + .007 });
            mapIcon3.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon3.Title = "Volunteer";
            mapIcon3.ZIndex = 0;

            MapIcon mapIcon4 = new MapIcon();
            mapIcon4.Location = coordinate.Coordinate.Point;
            mapIcon4.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon4.Title = "You";
            mapIcon4.ZIndex = 0;


            
            // Add the MapIcon to the map.
            MyMap.MapElements.Add(mapIcon1);
            MyMap.MapElements.Add(mapIcon2);
            MyMap.MapElements.Add(mapIcon3);
            MyMap.MapElements.Add(mapIcon4);
             
            await MyMap.TrySetViewAsync(coordinate.Coordinate.Point, 16D);


            
            //// Center the map over the POI.
            //MyMap.Center = coordinate.Coordinate.Point;

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

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EditUserProfile));
        }

        private void Request_Pickup_Click(object sender, RoutedEventArgs e)
        {
            ItemsPanel.Visibility = Visibility.Visible;
            GridLength gl = new GridLength(1, GridUnitType.Star);
            row2.Height = gl;
            btnCancel.Visibility = Visibility.Visible;
            btnContinue.Visibility = Visibility.Visible;

            MyMap.Visibility = Visibility.Collapsed;
            row1.Height = new GridLength(0);
            btnrequestpickup.Visibility = Visibility.Collapsed;

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            //ApplicationData.Current.LocalSettings.Values["composite"] = null;
            App.transaction_table = null;
            App.volunteer = null;
            App.driver = null;
            App.user = null;
            this.Frame.Navigate(typeof(Login));
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsValid())
            {
                App.transaction_table = new Transaction_Table();
                App.transaction_table.User_Id = App.user.User_Id;
                App.transaction_table.Time_Begin = System.DateTime.Now;
                App.transaction_table.Status = "Volunteer Requested";

                string latitude = coordinate.Coordinate.Latitude.ToString();
                string longitude = coordinate.Coordinate.Longitude.ToString();
                int lat_dec_index = latitude.IndexOf(".");
                int lon_dec_index = longitude.IndexOf(".");
                App.transaction_table.L_User = latitude.Substring(0, lat_dec_index + 4) + "," + longitude.Substring(0, lon_dec_index + 4);
                App.transaction_table.Insert();

                App.transaction_table = Transaction_Table.GetTransactionByUserId(App.user.User_Id);

                this.Frame.Navigate(typeof(UserSession));
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ItemsPanel.Visibility = Visibility.Collapsed;
            GridLength gl = new GridLength(0);
            row2.Height = gl;
            btnCancel.Visibility = Visibility.Collapsed;
            btnContinue.Visibility = Visibility.Collapsed;

            MyMap.Visibility = Visibility.Visible;
            row1.Height = new GridLength(1, GridUnitType.Star);
            btnrequestpickup.Visibility = Visibility.Visible;
        }

        //public void ConvertToComposite()
        //{
        //    user.User_Id = (int)composite["User_Id"];
        //    user.Password = (string)composite["Password"];
        //    user.Mobile_Number = (string)composite["Mobile_Number"];
        //    user.Mobile_Alternate = (string)composite["Mobile_Alternate"];
        //    user.F_Name = (string)composite["F_Name"];
        //    user.L_Name = (string)composite["L_Name"];
        //    user.Email = (string)composite["Email"];
        //    user.Close = (bool)composite["Close"];
        //    user.Area_Id = (int)composite["Area_Id"];
        //    user.Address_Line_2 = (string)composite["Address_Line_2"];
        //    user.Address_Line_1 = (string)composite["Address_Line_1"];
        //    user.Account_Status = (string)composite["Account_Status"];

        //}

        public bool ItemsValid()
        {
            txtItemsError.Text=RegexValidation.Check_Items(txtItems.Text);

            return txtItemsError.Text == "" ? true : false;
        }
    }
}
