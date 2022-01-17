using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDemoF.Views.Agenda
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class addAgenda : ContentPage
    {
        
        string nombre;
        string apellido;
        string correo;
        string cadena;
        int id_user;
        Services.ServiceAgenda ObjServiceAgenda;
        public addAgenda(string cadena)
        {
            InitializeComponent();
            this.cadena = cadena;
            ObjServiceAgenda = new Services.ServiceAgenda();
            datos(cadena);
        }

        private async void Btn_agenda(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new Views.Agenda.agenda(cadena));
        }

        private async void Btn_store(object sender, EventArgs e)
        {

            string mensaje = await ObjServiceAgenda.registrarAgenda(txtnombre.Text,txttelefono.Text,txtdireccion.Text,id_user.ToString());

            
            JObject json = JObject.Parse(mensaje);
           
            foreach (var datos in json)
            {
                var b = datos.ToString().Trim(new char[] { '[', ']' });
                string[] a = b.Split(',');
                switch (a[0])
                {
                    case "nombre":

                        lblmensajenombre.Text = a[1];
                        break;
                    case "telefono":

                        lblmensajetelefono.Text = a[1];
                        break;
                    case "direccion":

                        lblmensajedireccion.Text = a[1];
                        break;

                    case "message":


                        await DisplayAlert("Actualizada", a[1], "OK");

                        await Navigation.PushAsync(new Views.Agenda.agenda(cadena));
                        break;

                }



            }
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



    }
}