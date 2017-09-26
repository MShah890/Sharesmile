using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SS_DB_API.DTO_Models
{
    public class DTO_State
    {
        public int State_Id { get; set; }

        public string State_Name { get; set; }

        public IEnumerable<int> City_Id { get; set; }
    }
}