using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShareSmile.Models
{
    public class Volunteer
    {
        public int Volunteer_Id { get; set; }
        public int NGO_Id { get; set; }
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

        public async void Save_Volunteer()
        {
            string url = "http://" + App.serviceurl + "/api/Volunteers/PutVolunteer/" + this.Volunteer_Id;
            Uri BaseUri = new Uri(url);

            string json = "";
            json = JsonConvert.SerializeObject(this);
            var objclient = new HttpClient();
            HttpResponseMessage response = await objclient.PutAsync(BaseUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            string responseJsonText = await response.Content.ReadAsStringAsync();

        }

        public static Volunteer Get_VolunteerById(int id)
        {
            string url = "http://" + App.serviceurl + "/api/Volunteers/GetVolunteer/" + id;
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
                catch (System.AggregateException)
                {
                    return null;
                }

                Volunteer obj = JsonConvert.DeserializeObject<Volunteer>(response);
                return obj;

            }
        }
    }
}
