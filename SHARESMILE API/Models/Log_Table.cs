//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SS_DB_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Log_Table
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
    
        public virtual Driver Driver { get; set; }
        public virtual User User { get; set; }
        public virtual Volunteer Volunteer { get; set; }
    }
}