using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShareSmile.Models
{
    public class Log_Table
    {
        public int Log_Table_Id { get; set; }
        public Nullable<int> User_Id { get; set; }
        public Nullable<int> Volunteer_Id { get; set; }
        public Nullable<int> Driver_Id { get; set; }
        public Nullable<System.DateTime> Time_Begin { get; set; }
        public Nullable<System.DateTime> Time_Close { get; set; }
        public string Items { get; set; }
        public Nullable<bool> Successful { get; set; }
        public string Vehicle { get; set; }
        public string Status { get; set; }
        public string L_User { get; set; }
        public string L_Volunteer { get; set; }
        public string L_Driver { get; set; }

        public virtual Driver Driver { get; set; }
        public virtual User User { get; set; }
        public virtual Volunteer Volunteer { get; set; }


        public Log_Table() { }

        public Log_Table(Transaction_Table t)
        {
            User_Id = t.User_Id;
            Volunteer_Id = t.Volunteer_Id;
            Driver_Id = t.Driver_Id;
            Time_Begin = t.Time_Begin;
            Time_Close = t.Time_Close;
            Items = t.Items;
            Vehicle = t.Vehicle;
            Status = t.Status;
            L_User = t.L_User;
            L_Volunteer = t.L_Volunteer;
            L_Driver = t.L_Driver;    
        }

        public async void Insert()
        {
            Uri BaseUri = new Uri("http://" + App.serviceurl + "/api/Log_Table/PostLog_Table");

            string json = "";
            json = JsonConvert.SerializeObject(this);
            var objclient = new HttpClient();
            HttpResponseMessage response = await objclient.PostAsync(BaseUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            string responseJsonText = await response.Content.ReadAsStringAsync();

        }
        
    }

}
