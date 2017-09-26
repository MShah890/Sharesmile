using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SS_DB_API.DTO_Models
{
    //public class DTO_Transaction_Table
    //{
    //    public int Transaction_Id { get; set; }
    //    public Nullable<int> User_Id { get; set; }
    //    public Nullable<int> Volunteer_Id { get; set; }
    //    public Nullable<int> Driver_Id { get; set; }
    //    public Nullable<System.DateTime> Time_Begin { get; set; }
    //    public Nullable<System.DateTime> Time_Close { get; set; }
    //    public string Items { get; set; }
    //    public Nullable<bool> Successful { get; set; }
    //    public string Vehicle { get; set; }
    //    public string Status { get; set; }
    //    public string L_User { get; set; }
    //    public string L_Volunteer { get; set; }
    //    public string L_Driver { get; set; }
    //}

    public class DTO_Transaction_Table
    {
        public int Transaction_Id { get; set; }

        public Nullable<int> User_Id { get; set; }
        public string User_Name { get; set; }
        public string User_Mobile_Number { get; set; }

        public Nullable<int> Volunteer_Id { get; set; }
        public string Volunteer_Name { get; set; }
        public string Volunteer_Mobile { get; set; }

        public Nullable<int> Driver_Id { get; set; }
        public string Driver_Name { get; set; }
        public string Driver_Mobile { get; set; }

        public Nullable<System.DateTime> Time_Begin { get; set; }
        public Nullable<System.DateTime> Time_Close { get; set; }

        public string Items { get; set; }
        public Nullable<bool> Successful { get; set; }

        public string Vehicle { get; set; }
        public string Status { get; set; }

        public string L_User { get; set; }
        public string L_Volunteer { get; set; }
        public string L_Driver { get; set; }
    }

}