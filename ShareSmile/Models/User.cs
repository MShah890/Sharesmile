using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShareSmile.Models
{
    public class User
    {
        public int User_Id { get; set; }
        public string F_Name { get; set; }
        public string L_Name { get; set; }
        public string Address_Line_1 { get; set; }
        public string Address_Line_2 { get; set; }
        public int Area_Id { get; set; }
        public string Mobile_Number { get; set; }
        public string Mobile_Alternate { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Close { get; set; }
        public string Password { get; set; }
        public string Account_Status { get; set; }

        public async void Register_User()
        {
            Uri BaseUri = new Uri("http://" + App.serviceurl + "/api/Users/PostUser");
            
            string json = "";
            json = JsonConvert.SerializeObject(this);
            var objclient = new HttpClient();
            HttpResponseMessage response = await objclient.PostAsync(BaseUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            string responseJsonText = await response.Content.ReadAsStringAsync();

        }


        public async void Save_User()
        {
            string url = "http://" + App.serviceurl + "/api/Users/PutUser/" + this.User_Id;
            Uri BaseUri = new Uri(url);

            string json = "";
            json = JsonConvert.SerializeObject(this);
            var objclient = new HttpClient();
            HttpResponseMessage response = await objclient.PutAsync(BaseUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            string responseJsonText = await response.Content.ReadAsStringAsync();
         
        }

        public static User Get_UserById(int id)
        {
            string url = "http://" + App.serviceurl + "/api/Users/GetUser/"+id;
            Uri BaseUri = new Uri(url);

            using (var client = new HttpClient())
            {
                var response = "";

                try
                {
                    Task task = Task.Run(async () =>
                    {
                        response = await client.GetStringAsync(BaseUri);
                    });
                    task.Wait(30000);
                }
                catch (Exception)
                {
                    return null;
                }


                User obj = JsonConvert.DeserializeObject<User>(response);
                return obj;

            }
        }

        public void Edit_User(User user,int id)
        {

        }
    }

}
