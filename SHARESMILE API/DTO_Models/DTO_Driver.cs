using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SS_DB_API.DTO_Models
{
    public class DTO_Driver
    {
        public int Driver_Id { get; set; }
        public string F_Name { get; set; }
        public string L_Name { get; set; }
        public string Address_Line_1 { get; set; }
        public string Address_Line_2 { get; set; }
        public string Mobile_Number { get; set; }
        public string Mobile_Alternate { get; set; }
        public string Email { get; set; }
        public int Area_Id { get; set; }
        public Nullable<bool> Close { get; set; }
        public string Password { get; set; }
        public string Account_Status { get; set; }

        public IEnumerable<int> NGO_Id { get; set; }
    }
}