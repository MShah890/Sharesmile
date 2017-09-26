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
using ShareSmile.HelperClasses;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ShareSmile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserRegistration : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public User user;

        //public static List<State> allstates = Location.GetStates();
        //public static List<City> allcities = Location.GetCities();
        ////public static List<Area> allareas = Location.GetAreas();


        public UserRegistration()
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
            List<State> states = Location.GetStates();
            cbState.ItemsSource = states;
            cbState.SelectionChanged += cbState_SelectionChanged;
            cbState.SelectedIndex = 0;
            //cbCity.IsEnabled = false;
            
            //cbArea.IsEnabled = false;

            //txtFirst_Name.Text = "John";
            //txtLast_Name.Text = "Hancock";
            //txtMobile_Number.Text = "7564567890";
            //txtEmail.Text="john_hancock@gmail.com";
            //txtPassword.Password = txtConfirm_Password.Password = "Jo123_Ca";
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

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateUser())
            {
                User user = new User();
                user.F_Name = txtFirst_Name.Text;
                user.L_Name = txtLast_Name.Text;
                user.Mobile_Number = txtMobile_Number.Text;
                user.Email = txtEmail.Text;
                user.Password = txtPassword.Password;
                user.Mobile_Alternate = "";
                user.Account_Status = "Active";
                user.Address_Line_1 = "Address Line 1";
                user.Address_Line_2 = "Address Line 2";
                user.Area_Id = ((Area)cbArea.SelectedItem).Area_Id;

                user.Register_User();
            }
        }
        
        private void cbState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                State new_state = (State)cbState.SelectedItem;

                cbCity.SelectionChanged -= cbCity_SelectionChanged; 

                List<City> cities = Location.GetCityByStateId(new_state.State_Id);

                cbCity.ItemsSource = cities;
                
                //cbCity.IsEnabled = true;
                
                cbCity.SelectionChanged += cbCity_SelectionChanged;

                cbCity.SelectedIndex = 0;
                //cbArea.IsEnabled = false;
 
        }

        private void cbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            City new_city = (City)cbCity.SelectedItem;

            List<Area> areas = Location.GetAreaByCityId(new_city.City_Id);

            cbArea.ItemsSource = areas;

            cbArea.SelectedIndex = 0;
            //cbArea.IsEnabled = true;
            
        }

        public bool ValidateUser()
        {
            bool register = true;

            tblFirst_Name_Error.Text= RegexValidation.Name_Check(txtFirst_Name.Text);
            register = tblFirst_Name_Error.Text!="" ? false : register;

            tblLast_Name_Error.Text = RegexValidation.Name_Check(txtLast_Name.Text);
            register = tblLast_Name_Error.Text != "" ? false : register;

            tblMobile_Error.Text = RegexValidation.Check_Mobile(txtMobile_Number.Text);
            register = tblMobile_Error.Text != "" ? false : register;

            tblPassword_Error.Text = RegexValidation.Check_Password(txtPassword.Password);
            register = tblPassword_Error.Text != "" ? false : register;

            tblConfirmPassword_Error.Text = txtPassword.Password != txtConfirm_Password.Password ? "Passwords do not match" : "";
            register = tblConfirmPassword_Error.Text != "" ? false : register;

            tblState_Error.Text = cbState.SelectedIndex < 0 ? "State not selected" : "";
            register = tblState_Error.Text != "" ? false : register;

            tblCity_Error.Text = cbCity.IsEnabled == false || cbCity.SelectedIndex < 0 ? "City not selected" : "";
            register = tblCity_Error.Text != "" ? false : register;

            tblArea_Error.Text = cbArea.IsEnabled == false || cbArea.SelectedIndex < 0 ? "Area not selected" : "";
            register = tblArea_Error.Text != "" ? false : register;

            tblEmail_Error.Text = RegexValidation.Check_Email(txtEmail.Text);
            register = tblEmail_Error.Text != "" ? false : register;

            return register;
        }
    }
}
