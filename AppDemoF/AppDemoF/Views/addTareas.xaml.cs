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
    public partial class addTareas : ContentPage
    {
        string cadena;
        Services.ServiceTask ObjServicesTask;
        string nombre;
        string apellido;
        string correo;
        int id_user;
        public addTareas(string cadena)
        {
            InitializeComponent();
            this.cadena = cadena;
            ObjServicesTask = new Services.ServiceTask();
            datos(cadena);
        }
        private async void Btn_tareas(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new Views.tareas(cadena));
        }
        private async void Btn_saveTask(object sender, EventArgs e)
        {
            string mensaje=await ObjServicesTask.registrarTask(txttarea.Text, txtadescripcion.Text, id_user.ToString());
            JObject json = JObject.Parse(mensaje);
            limpiarErrorCampos();
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

                        lblmensajedescripcion.Text = a[1];
                        break;
                
                    case "message":
                       
                        await DisplayAlert("Tarea ","Registrada..!", "OK");
                        await Navigation.PushAsync(new Views.tareas(cadena));
                        break;

                }
            }

        }


        public void limpiarErrorCampos()
        {
            lblmensajetarea.Text = "";
            lblmensajedescripcion.Text = "";
        

        }

        public void datos(string cadena)
        {
            string[] auxcadena = cadena.Split(',');
            string[] tempId = auxcadena[0].Split(':');
            string[] tempnombre = auxcadena[1].Split(':');
            string[] tempapellido = auxcadena[2].Split(':');
            string[] tempcorreo = auxcadena[4].Split(':');

            string auxid = tempId[1].ToString().Trim(new char[] { '"' }).Trim();
            string auxnombre = tempnombre[1].ToString().Trim(new char[] { '"' }).Trim();
            string auxapellido = tempapellido[1].ToString().Trim(new char[] { '"' }).Substring(1).Trim();
            string auxcorreo = tempcorreo[1].ToString().Trim(new char[] { '"' }).Substring(1).Trim();
            id_user = int.Parse(auxid.Substring(1));
            nombre = auxnombre.Substring(1);
            apellido = auxapellido.Substring(1);
            correo = auxcorreo.Substring(1);
           
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}