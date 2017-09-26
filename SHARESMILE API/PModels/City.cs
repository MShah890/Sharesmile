using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SS_DB_API.DTO_Models;

namespace SS_DB_API.Models
{
    public partial class City
    {
        public void ConvertToDTO(DTO_City dcity)
        {
            dcity.City_Id = City_Id;
            dcity.City_Name = City_Name;
            dcity.State_Id = State_Id;
        }
    }
}