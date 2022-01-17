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
    public partial class editAgenda : ContentPage
    {
        string cadena;
        string id_agenda;
        Services.ServiceAgenda ObjSevicesAgenda;
        public editAgenda(string cadena, string id_agenda)
        {
            InitializeComponent();
            this.cadena = cadena;
            this.id_agenda = id_agenda;
            ObjSevicesAgenda = new Services.ServiceAgenda();
            Update();
        }
        private async void Btn_agenda(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new Views.Agenda.agenda(cadena));
        }
        private async void Btn_updateAgenda(object sender, EventArgs e)
        {


           string mensaje= await ObjSevicesAgenda.updateAgenda(txtnombre.Text, txttelefono.Text, txtdireccion.Text, id_agenda);
            JObject json = JObject.Parse(mensaje);
            Console.WriteLine(mensaje);
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





        public async void Update()
        {
            string aux = await ObjSevicesAgenda.selectAgendaId(id_agenda);
            string[] auxid = aux.Split(',');
            string[] temNombre = auxid[1].Split(':');
            string[] temTelefono = auxid[2].Split(':');
            string[] temDireccion = auxid[3].Split(':');
            string auxNombre = temNombre[1].Substring(1).Trim();
            string auxTelefono = temTelefono[1].Substring(1).Trim();
            string auxDireccion = temDireccion[1].Substring(1).Trim();
            txtnombre.Text = auxNombre.Remove(auxNombre.Length - 1);
            txttelefono.Text = auxTelefono.Remove(auxTelefono.Length - 1);
            txtdireccion.Text = auxDireccion.Remove(auxDireccion.Length - 1);
        }


    }
}