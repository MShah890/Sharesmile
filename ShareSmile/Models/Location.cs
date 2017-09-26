using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShareSmile.Models
{
    public static class Location
    {
        public static List<State> GetStates()
        {
            string url = "http://" + App.serviceurl + "/api/States/GetStates";
            Uri BaseUri = new Uri(url);

            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(BaseUri);
                });
                task.Wait();

                List<State> obj = JsonConvert.DeserializeObject<List<State>>(response);
                return obj;

            }
        }

        public static List<City> GetCities()
        {
            string url = "http://" + App.serviceurl + "/api/Cities/GetCities";
            Uri BaseUri = new Uri(url);

            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(BaseUri);
                });
                task.Wait();

                List<City> obj = JsonConvert.DeserializeObject<List<City>>(response);
                return obj;

            }
        }

        public static List<Area> GetAreas()
        {
            string url = "http://" + App.serviceurl + "/api/Areas/GetAreas";
            Uri BaseUri = new Uri(url);

            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(BaseUri);
                });
                task.Wait();

                List<Area> obj = JsonConvert.DeserializeObject<List<Area>>(response);
                return obj;

            }
        }

        public static State GetState(int id)
        {
            string State_Id = id.ToString();

            string url = "http://" + App.serviceurl + "/api/States/GetState/" + State_Id;
            Uri BaseUri = new Uri(url);

            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(BaseUri);
                });
                task.Wait();

                State obj = JsonConvert.DeserializeObject<State>(response);
                return obj;

            }
        }

        public static City GetCity(int id)
        {
            string City_Id = id.ToString();

            string url = "http://" + App.serviceurl + "/api/Cities/GetCity/" + City_Id;
            Uri BaseUri = new Uri(url);

            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(BaseUri);
                });
                task.Wait();

                City obj = JsonConvert.DeserializeObject<City>(response);
                return obj;

            }
        }

        public static Area GetArea(int id)
        {
            string Area_Id = id.ToString();

            string url = "http://" + App.serviceurl + "/api/Areas/GetArea/" + Area_Id;
            Uri BaseUri = new Uri(url);

            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(BaseUri);
                });
                task.Wait();

                Area obj = JsonConvert.DeserializeObject<Area>(response);
                return obj;

            }
        }

        public static List<City> GetCityByStateId(int id)
        {
            string State_Id = id.ToString();

            string url = "http://" + App.serviceurl + "/api/States/GetCityByStateId/" + State_Id;

            Uri BaseUri = new Uri(url);

                using (var client = new HttpClient())
                {
                    var response = "";
                    Task task = Task.Run(async () =>
                    {
                        response = await client.GetStringAsync(BaseUri);
                    });
                    task.Wait();

                    List<City> obj = JsonConvert.DeserializeObject<List<City>>(response);
                    return obj;

            }

        }

        public static List<Area> GetAreaByCityId(int id)
        {
            string City_Id = id.ToString();

            string url = "http://" + App.serviceurl + "/api/Cities/GetAreaByCityId/" + City_Id;

            Uri BaseUri = new Uri(url);

            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(BaseUri);
                });
                task.Wait();

                List<Area> obj = JsonConvert.DeserializeObject<List<Area>>(response);
                return obj;

            }

        }
    }
}
