using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDemoF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class editTareas : ContentPage
    {
        string cadena;
        string id_task;
        string task;
        string descripcion;
        Services.ServiceTask ObjServiceTask;
        public editTareas(string cadena,string id_task)
        {
            InitializeComponent();
            ObjServiceTask = new Services.ServiceTask();
            this.cadena=cadena;
            this.id_task = id_task;
        
            Update();
           
        }
        private async void Btn_Actualizar(object sender, EventArgs e)
        {
           string mensaje=await ObjServiceTask.updateTask(txttarea.Text, txtadescripcion.Text, id_task);

            //await Navigation.PushAsync(new Views.tareas(cadena));
            JObject json = JObject.Parse(mensaje);
            //limpiarCampos();
            foreach (var datos in json)
            {
                var b = datos.ToString().Trim(new char[] { '[', ']' });
                string[] a = b.Split(',');
                switch (a[0])
                {
                    case "task":

                        lblmensajetarea.Text = a[1];
                        break;
                    case "descripcion":

                        lblmensajedescripcionedit.Text = a[1];
                        break;
                   
                    case "message":


                        await DisplayAlert("Actualizada", a[1], "OK");

                        await Navigation.PushAsync(new Views.tareas(cadena));
                        break;

                }



            }
        }
        private async void Btn_tareas(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new Views.tareas(cadena));
        }
        private async void Btn_inicio(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new dashboard(cadena));
        }

        public async void Update()
        {
            string aux = await ObjServiceTask.selecTaskId(id_task);
            string []auxid = aux.Split(',');
            string[] temTask = auxid[1].Split(':');
            string[] temDescription = auxid[2].Split(':');
            string auxTask = temTask[1].Substring(1).Trim();
            string auxDescripcion= temDescription[1].Substring(1).Trim();
            txttarea.Text = auxTask.Remove(auxTask.Length-1);
            txtadescripcion.Text = auxDescripcion.Remove(auxDescripcion.Length-1);
        }
    }
}