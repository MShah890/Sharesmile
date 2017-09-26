using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SS_DB_API.DTO_Models;

namespace SS_DB_API.Models
{
    public partial class State
    {
        public void ConvertToDTO(DTO_State dstate)
        {
            dstate.State_Id = State_Id;
            dstate.State_Name = State_Name;
            dstate.City_Id = Cities.Select(x => x.City_Id).ToList();
        }
    }
}