using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SS_DB_API.DTO_Models;

namespace SS_DB_API.Models
{
    public partial class User
    {
        public void ConvertToDTO(DTO_User dtouser)
        {
            dtouser.Account_Status = Account_Status;
            dtouser.Address_Line_1 = Address_Line_1;
            dtouser.Address_Line_2 = Address_Line_2;
            dtouser.Area_Id = Area_Id;
            dtouser.Close = Close;
            dtouser.Email = Email;
            dtouser.F_Name = F_Name;
            dtouser.L_Name = L_Name;
            dtouser.Mobile_Alternate = Mobile_Alternate;
            dtouser.Mobile_Number = Mobile_Number;
            dtouser.Password = Password;
            dtouser.User_Id = User_Id;
        }

    }
}