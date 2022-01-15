using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppDemoF.Services
{
    public class ApiService
    {
        
        public async Task<List<Model.Users>> selectUser()
        {
            HttpClient cliente = new HttpClient();
            var url = "https://cooperativa-pugacho.herokuapp.com/apiall";

           
            var response = await cliente.GetStringAsync(url);
            var users = JsonConvert.DeserializeObject<List<Model.Users>>(response);
            
            return users;

        }



        public async Task < string> checkLogin(string email, string password)
        {
            
            var url = "https://cooperativa-pugacho.herokuapp.com/api/login";
            

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("correo",email),
                new KeyValuePair<string, string>("clave",password),
            };
            var request = new HttpRequestMessage(HttpMethod.Post,url);

            request.Content = new FormUrlEncodedContent(keyValues);
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
        public async Task<string> registrarUser(string email, string password,string nombre,string apellido)
        {

            var url = "https://cooperativa-pugacho.herokuapp.com/api/user/register";


            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("correo",email),
                new KeyValuePair<string, string>("clave",password),
                new KeyValuePair<string, string>("nombre",nombre),
                new KeyValuePair<string, string>("apellido",apellido),
            };
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = new FormUrlEncodedContent(keyValues);
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}
