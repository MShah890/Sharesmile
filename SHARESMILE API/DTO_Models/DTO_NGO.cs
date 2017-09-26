using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SS_DB_API.DTO_Models
{
    public class DTO_NGO
    {
        public int NGO_Id { get; set; }
        public string Name { get; set; }
        public string Address_Line_1 { get; set; }
        public string Address_Line_2 { get; set; }
        public string Mobile_Number { get; set; }
        public string Mobile_Alternate { get; set; }
        public int Area_Id { get; set; }
        public string NGO_Logo_File { get; set; }
        public string Email { get; set; }
        public string Website_URL { get; set; }
        public string Telephone { get; set; }
        public string Password { get; set; }
        public string Account_Status { get; set; }

        public IEnumerable<int> Driver_Id { get; set; }
    }
}