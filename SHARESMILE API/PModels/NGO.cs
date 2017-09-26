using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SS_DB_API.DTO_Models;

namespace SS_DB_API.Models
{
    public partial class NGO
    {
        public void ConvertToDTO(DTO_NGO dngo)
        {
            dngo.NGO_Id = NGO_Id;
            dngo.Name = Name;
            dngo.Address_Line_1 = Address_Line_1;
            dngo.Address_Line_2 = Address_Line_2;
            dngo.Mobile_Number = Mobile_Number;
            dngo.Mobile_Alternate = Mobile_Alternate;
            dngo.Area_Id = Area_Id;
            dngo.NGO_Logo_File = NGO_Logo_File;
            dngo.Email = Email;
            dngo.Website_URL = Website_URL;
            dngo.Telephone = Telephone;
            dngo.Password = Password;
            dngo.Account_Status = Account_Status;

            dngo.Driver_Id = NGO_DRIVER.Select(x => x.Driver_Id).ToList();
        }
    }
}