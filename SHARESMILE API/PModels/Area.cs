using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SS_DB_API.DTO_Models;

namespace SS_DB_API.Models
{
    public partial class Area
    {
        public void ConvertToDTO(DTO_Area darea)
        {
            darea.Area_Id = Area_Id;
            darea.Area_Name = Area_Name;
            darea.City_Id = City_Id;
        }

    }
}