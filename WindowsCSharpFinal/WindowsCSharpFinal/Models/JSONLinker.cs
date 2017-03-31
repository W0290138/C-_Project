using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WindowsCSharpFinal.Models
{
    class JSONLinker
    {
        //make a GET request
        public static async Task<T> Get<T>(string url)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);
            string result = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }
        //do a PUT
        public static async Task Put(object obj, string url)
        {
            HttpClient httpClient = new HttpClient();
            StringContent jsonString = new StringContent(JsonConvert.SerializeObject(obj));
            jsonString.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            await httpClient.PutAsync(url, jsonString);
        }
    }
}
