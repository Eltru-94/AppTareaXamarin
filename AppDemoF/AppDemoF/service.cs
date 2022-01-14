using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppDemoF
{
    class service
    {
       //HttpClient cliente = new HttpClient();

        public async Task<List<User>> selectUser()
        {
            var url = "https://cooperativa-pugacho.herokuapp.com/apiall";
           
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accpet", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
           
            string content = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<List<User>>(content);
            return resultado;
               
            

        }
    }
}
