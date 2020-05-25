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
    }
}
