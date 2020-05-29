using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Restaurant.Services
{
    public class HttpClientService
    {
        private static readonly HttpClient client = new HttpClient();

        static HttpClientService() {}

        public static async Task<T> Get<T>(string url) {
            var stringTask = await client.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<T>(stringTask);
            return result;
        }

        public static async Task Post(string url, object body)
        {
            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PostAsync(url, data);
        }

        public static async Task Put(string url, object body)
        {
            var json = JsonConvert.SerializeObject(body);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PutAsync(url, data);
        }
    }
}
