using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SS_DB_API.DTO_Models;

namespace SS_DB_API.Models
{
    public partial class Volunteer
    {
        public void ConvertToDTO(DTO_Volunteer dtovol)
        {
            dtovol.Account_Status = Account_Status;
            dtovol.Address_Line_1 = Address_Line_1;
            dtovol.Address_Line_2 = Address_Line_2;
            dtovol.Area_Id = Area_Id;
            dtovol.Close = Close;
            dtovol.Email = Email;
            dtovol.F_Name = F_Name;
            dtovol.L_Name = L_Name;
            dtovol.Mobile_Alternate = Mobile_Alternate;
            dtovol.Mobile_Number = Mobile_Number;
            dtovol.NGO_Id = NGO_Id;
            dtovol.Password = Password;
            dtovol.Volunteer_Id = Volunteer_Id;
        }
    }
}