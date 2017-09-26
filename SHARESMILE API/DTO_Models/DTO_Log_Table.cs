using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SS_DB_API.DTO_Models
{
    public class DTO_Log_Table
    {
        public int Log_Table_Id { get; set; }
        public Nullable<int> User_Id { get; set; }
        public Nullable<int> Volunteer_Id { get; set; }
        public Nullable<int> Driver_Id { get; set; }
        public Nullable<System.DateTime> Time_Begin { get; set; }
        public Nullable<System.DateTime> Time_Close { get; set; }
        public string Items { get; set; }
        public Nullable<bool> Successful { get; set; }
        public string Vehicle { get; set; }
        public string Status { get; set; }

    }
}