using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppDemoF.Services
{
    public class ServiceTask
    {
        public async Task<string> registrarTask(string task, string descripcion, string id_user)
        {

            var url = "https://cooperativa-pugacho.herokuapp.com/api/task/store";


            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("task",task),
                new KeyValuePair<string, string>("descripcion",descripcion),
                new KeyValuePair<string, string>("id_user",id_user),
                
            };
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = new FormUrlEncodedContent(keyValues);
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }


        public async Task<List<Model.Tasks>> allTaskUser(string id_user)
        {
            HttpClient cliente = new HttpClient();
            var url = "https://cooperativa-pugacho.herokuapp.com/api/task/allUser/"+id_user;


            var response = await cliente.GetStringAsync(url);
            var tasks = JsonConvert.DeserializeObject<List<Model.Tasks>>(response);

            return tasks;

        }

        public async Task<int> deletTask(string id_task)
        {
            HttpClient cliente = new HttpClient();
            var url = "https://cooperativa-pugacho.herokuapp.com/api/task/deletTask/" + id_task;


            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return 0;

        }

        public async Task<string> selecTaskId(string id_task)
        {
            HttpClient cliente = new HttpClient();
            var url = "https://cooperativa-pugacho.herokuapp.com/api/task/selectIdTask/" + id_task;


            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return content;

        }

        public async Task<string> updateTask(string task, string descripcion, string id_task)
        {

            var url = "https://cooperativa-pugacho.herokuapp.com/api/task/update";


            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("task",task),
                new KeyValuePair<string, string>("descripcion",descripcion),
                new KeyValuePair<string, string>("id_task",id_task),

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
