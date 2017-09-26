using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SS_DB_API.Models;

namespace SS_DB_API.DTO_Models
{
    public class DTO_City
    {
        public int City_Id { get; set; }

        public string City_Name { get; set; }

        public int State_Id { get; set; }
        
    }
   
}