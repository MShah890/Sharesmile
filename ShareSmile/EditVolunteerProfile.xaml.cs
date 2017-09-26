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
    public sealed partial class EditVolunteerProfile : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public Volunteer volunteer;

        public static List<State> allstates = Location.GetStates();
        public static List<City> allcities = Location.GetCities();
        public static List<Area> allareas = Location.GetAreas();

        public EditVolunteerProfile()
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
            volunteer = App.volunteer;

            txtAdrl_1.Text = volunteer.Address_Line_1;
            txtAdrl_2.Text = volunteer.Address_Line_2;

            if (volunteer.Email != null)
                txtEmail.Text = volunteer.Email;
            else
                txtEmail.Text = "";

            txtFirst_Name.Text = volunteer.F_Name;
            txtLast_Name.Text = volunteer.L_Name;

            if (volunteer.Mobile_Alternate != null)
                txtMobile_Alternate.Text = volunteer.Mobile_Alternate;
            else
                txtMobile_Alternate.Text = ""; txtMobile_Number.Text = volunteer.Mobile_Number;


            Area area = allareas.Where(x => x.Area_Id == volunteer.Area_Id).FirstOrDefault();
            City city = allcities.Where(x => x.City_Id == area.City_Id).FirstOrDefault();
            State state = allstates.Where(x => x.State_Id == city.State_Id).FirstOrDefault();

            List<State> states = allstates;
            List<City> cities = allcities.Where(x => x.State_Id == state.State_Id).ToList();
            List<Area> areas = allareas.Where(x => x.City_Id == city.City_Id).ToList();

            cbState.ItemsSource = states;
            cbState.SelectedIndex = states.IndexOf(states.Where(x => x.State_Id == state.State_Id).First());

            cbCity.ItemsSource = cities;
            cbCity.SelectedIndex = cities.IndexOf(cities.Where(x => x.City_Id == city.City_Id).First());

            cbArea.ItemsSource = areas;
            cbArea.SelectedIndex = areas.IndexOf(areas.Where(x => x.Area_Id == area.Area_Id).First());

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


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ValidProfile())
            {
                Volunteer evolunteer = new Volunteer();

                evolunteer.Volunteer_Id = volunteer.Volunteer_Id;
                evolunteer.Account_Status = volunteer.Account_Status;
                evolunteer.Address_Line_1 = txtAdrl_1.Text;
                evolunteer.Address_Line_2 = txtAdrl_2.Text;
                evolunteer.Area_Id = ((Area)cbArea.SelectedItem).Area_Id;
                evolunteer.Email = txtEmail.Text;
                evolunteer.F_Name = txtFirst_Name.Text;
                evolunteer.L_Name = txtLast_Name.Text;
                evolunteer.Mobile_Alternate = txtMobile_Alternate.Text;
                evolunteer.Mobile_Number = txtMobile_Number.Text;
                evolunteer.Password = volunteer.Password;
                evolunteer.Save_Volunteer();
            }
        }

        private void cbState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            State new_state = (State)cbState.SelectedItem;

            if (new_state != null)
            {
                List<City> cities = allcities.Where(x => x.State_Id == new_state.State_Id).ToList();
                cbCity.ItemsSource = cities;
                cbCity.SelectedIndex = 0;
                List<Area> areas = allareas.Where(x => x.City_Id == ((City)cbCity.SelectedItem).City_Id).ToList();
                cbArea.ItemsSource = areas;
                cbArea.SelectedIndex = 0;
            }
        }

        private void cbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            City new_city = (City)cbCity.SelectedItem;

            if (new_city != null)
            {
                List<Area> areas = allareas.Where(x => x.City_Id == new_city.City_Id).ToList();
                cbArea.ItemsSource = areas;
                cbArea.SelectedIndex = 0;
            }
        }


        public bool ValidProfile()
        {
            bool register = true;

            tblFirst_Name_Error.Text = RegexValidation.Name_Check(txtFirst_Name.Text);
            register = tblFirst_Name_Error.Text != "" ? false : register;

            tblLast_Name_Error.Text = RegexValidation.Name_Check(txtLast_Name.Text);
            register = tblLast_Name_Error.Text != "" ? false : register;

            tblMobile_Error.Text = RegexValidation.Check_Mobile(txtMobile_Number.Text, "volunteer", volunteer.Volunteer_Id);
            register = tblMobile_Error.Text != "" ? false : register;

            if(txtMobile_Alternate.Text!="")
                tblMobile_Alternate_Error.Text = RegexValidation.Check_Mobile(txtMobile_Alternate.Text, "volunteer", volunteer.Volunteer_Id);
            register = tblMobile_Alternate_Error.Text != "" ? false : register;

            tblState_Error.Text = cbState.SelectedIndex < 0 ? "State not selected" : "";
            register = tblState_Error.Text != "" ? false : register;

            tblCity_Error.Text = cbCity.IsEnabled == false || cbCity.SelectedIndex < 0 ? "City not selected" : "";
            register = tblCity_Error.Text != "" ? false : register;

            tbladrl1_Error.Text = RegexValidation.Check_Address(txtAdrl_1.Text);
            register = tbladrl1_Error.Text != "" ? false : register;

            tbladrl2_Error.Text = RegexValidation.Check_Address(txtAdrl_2.Text);
            register = tbladrl2_Error.Text != "" ? false : register;

            tblArea_Error.Text = cbArea.IsEnabled == false || cbArea.SelectedIndex < 0 ? "Area not selected" : "";
            register = tblArea_Error.Text != "" ? false : register;

            if(txtEmail.Text!="")
                tblEmail_Error.Text = RegexValidation.Check_Email(txtEmail.Text, "volunteer", volunteer.Volunteer_Id);
            register = tblEmail_Error.Text != "" ? false : register;

            return register;
        }
    }

}
