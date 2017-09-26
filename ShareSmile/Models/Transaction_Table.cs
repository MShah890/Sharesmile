using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShareSmile.Models
{
    public class Transaction_Table
    {
        public int Transaction_Id { get; set; }

        public Nullable<int> User_Id { get; set; }
        public string User_Name { get; set; }
        public string User_Mobile_Number { get; set; }

        public Nullable<int> Volunteer_Id { get; set; }
        public string Volunteer_Name { get; set; }
        public string Volunteer_Mobile { get; set; }

        public Nullable<int> Driver_Id { get; set; }
        public string Driver_Name { get; set; }
        public string Driver_Mobile { get; set; }

        public Nullable<System.DateTime> Time_Begin { get; set; }
        public Nullable<System.DateTime> Time_Close { get; set; }

        public string Items { get; set; }
        public Nullable<bool> Successful { get; set; }

        public string Vehicle { get; set; }
        public string Status { get; set; }

        public string L_User { get; set; }
        public string L_Volunteer { get; set; }
        public string L_Driver { get; set; }

        public async void Insert()
        {
            Uri BaseUri = new Uri("http://" + App.serviceurl + "/api/Transaction_Table/PostTransaction_Table");

            string json = "";
            json = JsonConvert.SerializeObject(this);
            var objclient = new HttpClient();
            HttpResponseMessage response = await objclient.PostAsync(BaseUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            string responseJsonText = await response.Content.ReadAsStringAsync();

        }

        public string Update()
        {
            string uri = "http://" + App.serviceurl + "/api/Transaction_Table/PutTransaction_Table/" + this.Transaction_Id;
            Uri BaseUri = new Uri(uri);

            string json = "";
            json = JsonConvert.SerializeObject(this);
            var client = new HttpClient();

            HttpResponseMessage response;
            string responseJsonText = "";

            try
            {
                Task task = Task.Run(async () =>
                {
                    response = await client.PutAsync(BaseUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
                    responseJsonText = await response.Content.ReadAsStringAsync();

                });
                task.Wait(30000);

            }
            catch (System.AggregateException)
            {
                return null;
            }


            return responseJsonText;

        }

        public static List<Transaction_Table> Get_Transactions(string user_type)
        {
            string url;
            if (user_type == "volunteer")
                url = "http://" + App.serviceurl + "/api/Transaction_Table/GetTransaction_TableVolunteers";
            else
                url = "http://" + App.serviceurl + "/api/Transaction_Table/GetTransaction_TableDrivers";

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
                
                List<Transaction_Table> obj = JsonConvert.DeserializeObject<List<Transaction_Table>>(response);
                return obj;

            }

        }

        public static Transaction_Table GetTransactionById(int id)
        {
            string url;

            url = "http://" + App.serviceurl + "/api/Transaction_Table/GetTransaction_Table/"+id;

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
                catch(System.AggregateException)
                {
                    return null;
                }
               
                Transaction_Table obj = JsonConvert.DeserializeObject<Transaction_Table>(response);
                return obj;

            }
        }

        public static Transaction_Table GetTransactionByUserId(int id)
        {
            string url;

            url = "http://" + App.serviceurl + "/api/Transaction_Table/GetTransaction_TableByUser/" + id;

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
                
                Transaction_Table obj = JsonConvert.DeserializeObject<Transaction_Table>(response);
                return obj;

            }
        }

        public static async void Delete(int id)
        {
            Uri BaseUri = new Uri("http://" + App.serviceurl + "/api/Transaction_Table/DeleteTransaction_Table/"+id);
            
            var objclient = new HttpClient();
            HttpResponseMessage response=await objclient.DeleteAsync(BaseUri); 
            string responseJsonText = await response.Content.ReadAsStringAsync();

        }
    }
}
