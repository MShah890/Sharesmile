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
using Windows.Storage;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ShareSmile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditUserProfile : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public User user;

        public static List<State> allstates = Location.GetStates();
        public static List<City> allcities = Location.GetCities();
        public static List<Area> allareas = Location.GetAreas();

        public EditUserProfile()
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
            //user = (User)e.NavigationParameter;
            //composite = (ApplicationDataCompositeValue)ApplicationData.Current.LocalSettings.Values["User_Session"];
            //ConvertToComposite();

            user = App.user;

            txtAdrl_1.Text = user.Address_Line_1;
            txtAdrl_2.Text = user.Address_Line_2;

            if (user.Email != null)
                txtEmail.Text = user.Email;
            else
                txtEmail.Text = "";

            txtFirst_Name.Text = user.F_Name;
            txtLast_Name.Text = user.L_Name;

            if (user.Mobile_Alternate != null)
                txtMobile_Alternate.Text = user.Mobile_Alternate;
            else
                txtMobile_Alternate.Text = "";

            txtMobile_Number.Text = user.Mobile_Number;

            Area area = allareas.Where(x => x.Area_Id == user.Area_Id).FirstOrDefault();
            City city = allcities.Where(x => x.City_Id == area.City_Id).FirstOrDefault();
            State state = allstates.Where(x => x.State_Id == city.State_Id).FirstOrDefault();

            List<State> states = allstates;
            List<City> cities = allcities.Where(x => x.State_Id == state.State_Id).ToList();
            List<Area> areas =  allareas.Where(x=>x.City_Id==city.City_Id).ToList();

            cbState.ItemsSource = states;
            cbState.SelectedIndex = states.IndexOf(states.Where(x=>x.State_Id==state.State_Id).First());

            cbCity.ItemsSource = cities;
            cbCity.SelectedIndex = cities.IndexOf(cities.Where(x=>x.City_Id==city.City_Id).First());

            cbArea.ItemsSource = areas;
            cbArea.SelectedIndex = areas.IndexOf(areas.Where(x=>x.Area_Id==area.Area_Id).First());

            
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
            User euser = new User();
            if (ValidProfile())
            {
                euser.User_Id = user.User_Id;
                euser.Account_Status = user.Account_Status;
                euser.Address_Line_1 = txtAdrl_1.Text;
                euser.Address_Line_2 = txtAdrl_2.Text;
                euser.Area_Id = ((Area)cbArea.SelectedItem).Area_Id;
                euser.Email = txtEmail.Text;
                euser.F_Name = txtFirst_Name.Text;
                euser.L_Name = txtLast_Name.Text;
                euser.Mobile_Alternate = txtMobile_Alternate.Text;
                euser.Mobile_Number = txtMobile_Number.Text;
                euser.Password = user.Password;
                euser.Save_User();
            }
        }

        private void cbState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            State new_state = (State)cbState.SelectedItem;

            if(new_state != null)
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

            tblMobile_Error.Text = RegexValidation.Check_Mobile(txtMobile_Number.Text, "user",user.User_Id);
            register = tblMobile_Error.Text != "" ? false : register;

            if (txtMobile_Alternate.Text != "")
                tblMobile_Alternate_Error.Text = RegexValidation.Check_Mobile(txtMobile_Alternate.Text,"user",user.User_Id);
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
                tblEmail_Error.Text = RegexValidation.Check_Email(txtEmail.Text,"user",user.User_Id);
            register = tblEmail_Error.Text != "" ? false : register;

            return register;
        }

        //public void ConvertToComposite()
        //{
        //    user.User_Id=(int)composite["User_Id"];
        //    user.Password=(string)composite["Password"];
        //    user.Mobile_Number= (string)composite["Mobile_Number"];
        //    user.Mobile_Alternate= (string)composite["Mobile_Alternate"];
        //    user.F_Name= (string)composite["F_Name"];
        //    user.L_Name= (string)composite["L_Name"];
        //    user.Email= (string)composite["Email"];
        //    user.Close= (bool)composite["Close"];
        //    user.Area_Id= (int)composite["Area_Id"];
        //    user.Address_Line_2= (string)composite["Address_Line_2"];
        //    user.Address_Line_1= (string)composite["Address_Line_1"];
        //    user.Account_Status= (string)composite["Account_Status"];

        //}


    }
}
