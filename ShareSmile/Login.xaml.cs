﻿using ShareSmile.Common;
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
using ShareSmile;
using Newtonsoft.Json;
using Windows.Storage;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ShareSmile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public Login()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            mystackpanel.DataContext = this;
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

        private void ForgotPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ForgotPassword));
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserRegistration));
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginValidation lv = new LoginValidation();

            //object res= lv.Validate_Login(Mobile_Email.Text, Password.Password);
            object res = lv.Validate_Login("7567731118", "xyz");
            //object res = lv.Validate_Login("kara@hotmail.com", "xyz");
            //object res = lv.Validate_Login("prakash@hotmail.com", "xyz");
            //var localsettings = ApplicationData.Current.LocalSettings;

            //ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue();
            //composite["intVal"] = 1;
            //composite["strVal"] = "string";
            if (res == null)
            {
                tblError.Text = "Incorrect Email/Mobile or Password";
            }
            else
            {
                Custom result = (Custom)res;
                //Custom result = lv.Validate_Login("7567731118", "xyz");
                //Custom result = lv.Validate_Login("8827916189", "As1234_kjk");
                //Custom result = lv.Validate_Login("4567877898", "xyz");

                if (result.type == "volunteer")
                {
                    Volunteer volunteer = JsonConvert.DeserializeObject<Volunteer>(result.obj.ToString());
                    App.volunteer = volunteer;
                    //composite["Volunteer_Id"] = volunteer.Volunteer_Id;
                    //composite["Password"] = volunteer.Password;
                    //composite["NGO_Id"] = volunteer.NGO_Id;
                    //composite["Mobile_Number"] = volunteer.Mobile_Number;
                    //composite["Mobile_Alternate"] = volunteer.Mobile_Alternate;
                    //composite["F_Name"] = volunteer.F_Name;
                    //composite["L_Name"] = volunteer.L_Name;
                    //composite["Email"] = volunteer.Email;
                    //composite["Close"] = volunteer.Close;
                    //composite["Area_Id"] = volunteer.Area_Id;
                    //composite["Address_Line_2"] = volunteer.Address_Line_2;
                    //composite["Address_Line_1"] = volunteer.Address_Line_1;
                    //composite["Account_Status"] = volunteer.Account_Status;

                    //localsettings.Values["Volunteer_Session"] = composite;

                    this.Frame.Navigate(typeof(VolunteerHome));
                }
                else if (result.type == "user")
                {
                    User user = JsonConvert.DeserializeObject<User>(result.obj.ToString());
                    App.user = user;
                    //composite["User_Id"] = user.User_Id;
                    //composite["Password"] = user.Password;
                    //composite["Mobile_Number"] = user.Mobile_Number;
                    //composite["Mobile_Alternate"] = user.Mobile_Alternate;
                    //composite["F_Name"] = user.F_Name;
                    //composite["L_Name"] = user.L_Name;
                    //composite["Email"] = user.Email;
                    //composite["Close"] = user.Close;
                    //composite["Area_Id"] = user.Area_Id;
                    //composite["Address_Line_2"] = user.Address_Line_2;
                    //composite["Address_Line_1"] = user.Address_Line_1;
                    //composite["Account_Status"] = user.Account_Status;

                    //localsettings.Values["User_Session"] = composite;

                    this.Frame.Navigate(typeof(UserHome));
                }
                else if (result.type == "driver")
                {
                    Driver driver = JsonConvert.DeserializeObject<Driver>(result.obj.ToString());
                    App.driver = driver;
                    //composite["Driver_Id"] = driver.Driver_Id;
                    //composite["Password"] = driver.Password;
                    //composite["Mobile_Number"] = driver.Mobile_Number;
                    //composite["Mobile_Alternate"] = driver.Mobile_Alternate;
                    //composite["F_Name"] = driver.F_Name;
                    //composite["L_Name"] = driver.L_Name;
                    //composite["Email"] = driver.Email;
                    //composite["Close"] = driver.Close;
                    //composite["Area_Id"] = driver.Area_Id;
                    //composite["Address_Line_2"] = driver.Address_Line_2;
                    //composite["Address_Line_1"] = driver.Address_Line_1;
                    //composite["Account_Status"] = driver.Account_Status;

                    //localsettings.Values["Driver_Session"] = composite;

                    this.Frame.Navigate(typeof(DriverHome));
                }
            }
        }
        
    }

    public class Custom
    {
        public object obj { get; set; }

        public string type { get; set; }
    }


}
