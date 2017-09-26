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
using Windows.UI.Popups;
using Windows.Services.Maps;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ShareSmile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DriverSession : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public Geolocator geolocator;
        public Geoposition coordinate;

        public DriverSession()
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
            App.transaction_table = Transaction_Table.GetTransactionById(App.transaction_table.Transaction_Id);

            if (App.transaction_table != null)
            {
                txtusername.Text = App.transaction_table.User_Name;
                txtusermobile.Text = App.transaction_table.User_Mobile_Number;
                txtitems.Text = App.transaction_table.Items;

                txtvolname.Text = App.transaction_table.Volunteer_Name;
                txtvolmobile.Text = App.transaction_table.Volunteer_Mobile;


                SetMap();
            }
            else
            {
                var dialog = new MessageDialog("Session has been cancelled");
                dialog.Commands.Add(new UICommand("OK"));
                await dialog.ShowAsync();
                this.Frame.Navigate(typeof(DriverHome));
            }
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

        private void CanSession_Click(object sender, RoutedEventArgs e)
        {
            App.transaction_table.Driver_Id = null;
            App.transaction_table.L_Driver = null;

            App.transaction_table.Status = "Driver Requested";
            App.transaction_table.Update();

            this.Frame.Navigate(typeof(DriverHome));
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DriverSession));
        }

        public async void SetMap()
        {
            MapService.ServiceToken = "Ajh0II1uJBt4Y_waGLLyq5R3ftW8N_x1NWUTsXq3m2pInKFpPw6sBctqeDG0RqFI";
            geolocator = new Geolocator();

            coordinate = await geolocator.GetGeopositionAsync();

            // Create a MapIcon.
            MapIcon mapIcon1 = new MapIcon();

            mapIcon1.Location = coordinate.Coordinate.Point;
            mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon1.Title = "You";
            mapIcon1.ZIndex = 0;
            MyMap.MapElements.Add(mapIcon1);

            MapIcon mapIcon2 = new MapIcon();

            string[] commapos = App.transaction_table.L_User.Split(",".ToCharArray());
            double ulat = double.Parse(commapos[0]);
            double ulond = double.Parse(commapos[1]);
            mapIcon2.Location = new Geopoint(new BasicGeoposition { Latitude = ulat, Longitude = ulond });
            mapIcon2.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon2.Title = "User";
            mapIcon2.ZIndex = 0;

            // Add the MapIcon to the map.
            MyMap.MapElements.Add(mapIcon2);

            MapIcon mapIcon3 = new MapIcon();

            commapos = App.transaction_table.L_Volunteer.Split(",".ToCharArray());
            double vlat = double.Parse(commapos[0]);
            double vlond = double.Parse(commapos[1]);
            mapIcon3.Location = new Geopoint(new BasicGeoposition { Latitude = vlat, Longitude = vlond });
            mapIcon3.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon3.Title = "Volunteer";
            mapIcon3.ZIndex = 0;

            // Add the MapIcon to the map.
            MyMap.MapElements.Add(mapIcon3);


            await MyMap.TrySetViewAsync(coordinate.Coordinate.Point, 16D);

            BasicGeoposition userlocation = new BasicGeoposition() { Latitude = ulat, Longitude = ulond };
            BasicGeoposition volunteerlocation = new BasicGeoposition() { Latitude = vlat, Longitude = vlond };
            // Reverse geocode the specified geographic location.
            MapLocationFinderResult uresult =
                await MapLocationFinder.FindLocationsAtAsync(new Geopoint(userlocation));
            MapLocationFinderResult vresult =
                await MapLocationFinder.FindLocationsAtAsync(new Geopoint(volunteerlocation));

            // If the query returns results, display the name of the town
            // contained in the address of the first result.
            if (uresult.Status == MapLocationFinderStatus.Success)
            {
                txtuseraddress.Text = uresult.Locations[0].Address.StreetNumber + " " + uresult.Locations[0].Address.Street + " " + uresult.Locations[0].Address.Neighborhood + " " + uresult.Locations[0].Address.Region;
            }
            if (vresult.Status == MapLocationFinderStatus.Success)
            {
                txtvoladdress.Text = vresult.Locations[0].Address.StreetNumber + " " + vresult.Locations[0].Address.Street + " " + vresult.Locations[0].Address.Neighborhood + " " + vresult.Locations[0].Address.Region;
            }
        }
    }
}
