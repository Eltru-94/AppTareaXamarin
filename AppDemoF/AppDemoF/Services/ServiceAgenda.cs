using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppDemoF.Services
{
    public class ServiceAgenda
    {


        public async Task<List<Model.Agenda>> allAgendaUser(string id_user)
        {
            HttpClient cliente = new HttpClient();
            var url = "https://cooperativa-pugacho.herokuapp.com/api/agenda/allUser/" + id_user;


            var response = await cliente.GetStringAsync(url);
            var agenda = JsonConvert.DeserializeObject<List<Model.Agenda>>(response);

            return agenda;

        }

        public async Task<int> deletAgenda(string id_agenda)
        {
            HttpClient cliente = new HttpClient();
            var url = "https://cooperativa-pugacho.herokuapp.com/api/agenda/delete/" + id_agenda;


            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return 0;

        }

        public async Task<string> registrarAgenda(string nombre, string telefono,string direccion,string id_user)
        {

            var url = "https://cooperativa-pugacho.herokuapp.com/api/agenda/store";


            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("id_user",id_user),
                new KeyValuePair<string, string>("nombre",nombre),
                new KeyValuePair<string, string>("telefono",telefono),
                new KeyValuePair<string, string>("direccion",direccion),

            };
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = new FormUrlEncodedContent(keyValues);
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public async Task<string> selectAgendaId(string id_agenda)
        {
            HttpClient cliente = new HttpClient();
            var url = "https://cooperativa-pugacho.herokuapp.com/api/agenda/agendaID/" + id_agenda;


            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return content;

        }

        public async Task<string> updateAgenda(string nombre, string telefono, string direccion, string id_agenda)
        {

            var url = "https://cooperativa-pugacho.herokuapp.com/api/agenda/update";


            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("nombre",nombre),
                new KeyValuePair<string, string>("telefono",telefono),
                new KeyValuePair<string, string>("direccion",direccion),
                new KeyValuePair<string, string>("id_agenda",id_agenda),

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
