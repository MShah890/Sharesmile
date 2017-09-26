using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SS_DB_API.DTO_Models;

namespace SS_DB_API.Models
{
    public partial class Transaction_Table
    {
        public void ConvertToDTO(DTO_Transaction_Table dtt)
        {
            dtt.Driver_Id = Driver_Id;
            dtt.Items = Items;
            dtt.L_Driver = L_Driver;
            dtt.L_User = L_User;
            dtt.L_Volunteer = L_Volunteer;
            dtt.Status = Status;
            dtt.Successful = Successful;
            dtt.Time_Begin = Time_Begin;
            dtt.Time_Close = Time_Close;
            dtt.Transaction_Id = Transaction_Id;
            dtt.User_Id = User_Id;
            dtt.Vehicle = Vehicle;
            dtt.Volunteer_Id = Volunteer_Id;
        }
    }
}