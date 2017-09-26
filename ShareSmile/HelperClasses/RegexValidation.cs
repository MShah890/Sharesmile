using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Globalization;

namespace ShareSmile.HelperClasses
{
    public static class RegexValidation
    {
        public static string Name_Check(string F_Name)
        {
            if (F_Name.Length == 0)
            {
                return "Field is empty";
            }
            else if (!Regex.IsMatch(F_Name, @"^[A-Z][a-z]+$"))
            {
                return "Incorrect Input Format Eg: John";
            }
            else if (F_Name.Length > 50)
            {
                return "Input exceeds limit of 50 characters";
            }
            else
            { 
                return "";
            }
           
        }

        public static string Check_Address(string Address)
        {
            if (Address.Length == 0)
            {
                return "Field Is Empty";
            }
            else if (!Regex.IsMatch(Address, @"^[A-Za-z0-9\s\-]+$"))
            {
                return "Only spaces,hyphen and alphanumeric characters are allowed";
            }
            else if (Address.Length > 500)
            {
                return "Input exceeds limit of 500 characters";
            }
            else
            {
                return "";
            }
        }

        public static string Check_Items(string items)
        {
            if (items.Length == 0)
            {
                return "Field Is Empty";
            }
            else if (!Regex.IsMatch(items, @"^[A-Za-z0-9\s\-]+$"))
            {
                return "Only spaces,hyphen and alphanumeric characters are allowed";
            }
            else if (items.Length > 500)
            {
                return "Input exceeds limit of 500 characters";
            }
            else
            {
                return "";
            }
        }

        public static string Check_Mobile(string Mobile)
        {
            if (Mobile.Length == 0)
            {
                return "Field is Empty";
            }
            else if (!Regex.IsMatch(Mobile, @"^[0-9]{10}$"))
            {
                return "Mobile number should be digits and length should be 10";
            }
            else if (Is_Mobile_Duplicate(Mobile))
            {
                return "Mobile Number Already Exists";
            }
            else
            {
                return "";
            }
        }


        public static bool Is_Mobile_Duplicate(string Mobile)
        {

            string url = "http://localhost:56545/api/Validate/CheckMobileDuplicate/" + Mobile;
            Uri BaseUri = new Uri(url);

            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(BaseUri);
                });
                task.Wait();

                bool obj = JsonConvert.DeserializeObject<bool>(response);
                return obj;

            }

        }

        //For Edit Profile
        public static string Check_Mobile(string Mobile,string user_type,int id)
        {
            if (Mobile.Length == 0)
            {
                return "Field is Empty";
            }
            else if (!Regex.IsMatch(Mobile, @"^[0-9]{10}$"))
            {
                return "Mobile number should be digits and length should be 10";
            }
            else if (Is_Mobile_Duplicate(Mobile, user_type, id))
            {
                return "Mobile Number Already Exists";
            }
            else
            {
                return "";
            }
        }

        //For Edit
        public static bool Is_Mobile_Duplicate(string Mobile,string user_type,int id)
        {
            string url;
            if (user_type == "user")
                url = "http://localhost:56545/api/Validate/CheckMobileDuplicateUserEdit/" + id + "/" + Mobile;
            else if (user_type == "volunteer")
                url = "http://localhost:56545/api/Validate/CheckMobileDuplicateVolunteerEdit/" + id + "/" + Mobile;
            else
                url = "http://localhost:56545/api/Validate/CheckMobileDuplicateDriverEdit/" + id + "/" + Mobile;

            Uri BaseUri = new Uri(url);

            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(BaseUri);
                });
                task.Wait();

                bool obj = JsonConvert.DeserializeObject<bool>(response);
                return obj;

            }

        }

        public static string Check_Email(string Email)
        {
            if (Email.Length == 0)
            {
                return "Field is Empty";
            }
            else if (!EmailValidation.IsValidEmail(Email))
            {
                return "Invalid Email";
            }
            else if (Is_Email_Duplicate(Email))
            {
                return "Email Id Already Exists";
            }
            else
            {
                return "";
            }
        }

        private static bool Is_Email_Duplicate(string Email)
        {
            Email=Email.Remove(Email.Length - 4);
            string url = "http://localhost:56545/api/Validate/CheckEmailDuplicate/" + Email;
            Uri BaseUri = new Uri(url);

            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(BaseUri);
                });
                task.Wait();

                bool obj = JsonConvert.DeserializeObject<bool>(response);
                return obj;

            }
        }

        public static string Check_Email(string Email,string user_type,int id)
        {
            if (Email.Length == 0)
            {
                return "Field is Empty";
            }
            else if (!EmailValidation.IsValidEmail(Email))
            {
                return "Invalid Email";
            }
            else if (Is_Email_Duplicate(Email,user_type,id))
            {
                return "Email Id Already Exists";
            }
            else
            {
                return "";
            }
        }

        private static bool Is_Email_Duplicate(string Email,string user_type,int id)
        {
            Email = Email.Remove(Email.Length - 4);
            string url;
            if (user_type == "user")
                url = "http://localhost:56545/api/Validate/CheckEmailDuplicateEditUser/" + id + "/" + Email;
            else if (user_type == "volunteer")
                url = "http://localhost:56545/api/Validate/CheckEmailDuplicateEditVolunteer/" + id + "/" + Email;
            else
                url = "http://localhost:56545/api/Validate/CheckEmailDuplicateEditDriver/" + id + "/" + Email;

            Uri BaseUri = new Uri(url);

            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(BaseUri);
                });
                task.Wait();

                bool obj = JsonConvert.DeserializeObject<bool>(response);
                return obj;

            }
        }

        public static string Check_Password(string Password)
        {
            if (Password.Length == 0)
            {
                return "Field Is Empty";
            }
            else if (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$"))
            {
                return "Password must contain One uppercase,one lowercase,one special character,one digit and length must be between 8 and 15 characters";
            }
            else
            {
                return "";
            }
        }
    }

    public static class EmailValidation
    {
        public static bool IsValidEmail(string strIn)
        {            
            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }

}
