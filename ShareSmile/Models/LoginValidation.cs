using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using ShareSmile;

namespace ShareSmile.Models
{
    class LoginValidation
    {

        public Custom Validate_Login(string Mobile_Email, string Password)
        {

            if (Mobile_Email == "" || Password == "")
                return null;

            string url = "http://" + App.serviceurl + "/api/Validate/GetLogin/" + Mobile_Email + "/" + Password;
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
               
                object error = JsonConvert.DeserializeObject<object>(response);

                if (error.GetType() == typeof(string))
                    return null;
                else
                {
                    Custom obj = JsonConvert.DeserializeObject<Custom>(response);
                    return obj;
                }
            }

        }
    }
}
