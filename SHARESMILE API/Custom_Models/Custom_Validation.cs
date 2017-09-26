using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SS_DB_API.Models;

namespace SS_DB_API.Custom_Models
{
    public class Custom_Validation
    {
        public List<Volunteer> Volunteers { get; set; }
        public List<User> Users { get; set; }
        public List<Driver> Drivers { get; set; }

        public object Login_Validation(string Mobile_Email,string Password)
        {
            long result;
            bool ifmobile = long.TryParse(Mobile_Email, out result);

            Predicate<Volunteer> mob_vol = x => (x.Mobile_Number == Mobile_Email || x.Mobile_Alternate == Mobile_Email) && x.Password == Password;
            Predicate<Volunteer> email_vol = x => x.Email == Mobile_Email&&x.Password==Password;

            Predicate<User> mob_user = x => (x.Mobile_Number == Mobile_Email || x.Mobile_Alternate == Mobile_Email) && x.Password == Password;
            Predicate<User> email_user = x => x.Email == Mobile_Email && x.Password == Password;

            Predicate<Driver> mob_dri = x => (x.Mobile_Number == Mobile_Email || x.Mobile_Alternate == Mobile_Email) && x.Password == Password;
            Predicate<Driver> email_dri = x => x.Email == Mobile_Email && x.Password == Password;

            if (ifmobile)
            {
                if (Volunteers.Exists(mob_vol))
                {
                    return Volunteers.Where(x => mob_vol(x)).FirstOrDefault(); 
                }
                else if(Users.Exists(mob_user))
                {
                    return Users.Where(x => mob_user(x)).FirstOrDefault();
                }
                else if(Drivers.Exists(mob_dri))
                {
                    return Drivers.Where(x => mob_dri(x)).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (Volunteers.Exists(email_vol))
                {
                    return Volunteers.Where(x => email_vol(x)).FirstOrDefault();
                }
                else if (Users.Exists(email_user))
                {
                    return Users.Where(x => email_user(x)).FirstOrDefault();
                }
                else if (Drivers.Exists(email_dri))
                {
                    return Drivers.Where(x => email_dri(x)).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }  
    }
}