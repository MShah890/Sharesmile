using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SS_DB_API.DTO_Models;

namespace SS_DB_API.Models
{
    public partial class Driver
    {
        public void ConvertToDTO(DTO_Driver dtodriver)
        {
            dtodriver.Driver_Id = Driver_Id;
            dtodriver.Account_Status = Account_Status;
            dtodriver.Address_Line_1 = Address_Line_1;
            dtodriver.Address_Line_2 = Address_Line_2;
            dtodriver.Area_Id = Area_Id;
            dtodriver.Close = Close;
            dtodriver.Email = Email;
            dtodriver.F_Name = F_Name;
            dtodriver.L_Name = L_Name;
            dtodriver.Mobile_Alternate = Mobile_Alternate;
            dtodriver.Mobile_Number = Mobile_Number;
            dtodriver.Password = Password;

            dtodriver.NGO_Id = NGO_DRIVER.Select(x => x.NGO_Id).ToList();  
        }
    }
}