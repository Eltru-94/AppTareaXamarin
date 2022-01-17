using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDemoF.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class editUser : ContentPage
    {
        string nombre;
        string apellido;
        string correo;
        string cadena;
        int id_user;
        Services.ApiService ObjServiceUser;
        public editUser(string cadena)
        {
            InitializeComponent();
            ObjServiceUser = new Services.ApiService();
            this.cadena = cadena;
            datos(cadena);
           
        }

        public void datos(string cadena)
        {
            string[] auxcadena = cadena.Split(',');
            string[] tempId = auxcadena[0].Split(':');
            string[] tempnombre = auxcadena[1].Split(':');
            string[] tempapellido = auxcadena[2].Split(':');
            string[] temcedula = auxcadena[3].Split(':');
            string[] tempcorreo = auxcadena[4].Split(':');

            string auxid = tempId[1].ToString().Trim(new char[] { '"' }).Trim();
            string auxnombre = tempnombre[1].ToString().Trim(new char[] { '"' }).Trim();
            string auxapellido = tempapellido[1].ToString().Trim(new char[] { '"' }).Substring(1).Trim();
            string auxcorreo = tempcorreo[1].ToString().Trim(new char[] { '"' }).Substring(1).Trim();
            string auxcedula = temcedula[1].ToString().Trim(new char[] { '"' }).Substring(1).Trim();
            id_user = int.Parse(auxid.Substring(1));
            nombre = auxnombre.Substring(1);
            apellido = auxapellido.Substring(1);
            correo = auxcorreo.Substring(1);

            txtnombre.Text = nombre;
            txtapellido.Text = apellido;
            txtcorreo.Text = correo;
            txtcedula.Text = auxcedula.Substring(1);
            
        }

        private async void update(object sender, EventArgs e)
        {



            string nombre = txtnombre.Text;
            string apellido = txtapellido.Text;

            string mensaje = await  ObjServiceUser.updateUser(txtcorreo.Text, txtpassword.Text, txtnombre.Text, txtapellido.Text, txtcedula.Text, txtcpassword.Text,id_user.ToString());
            JObject json = JObject.Parse(mensaje);
            limpiarErrorCampos();
            foreach (var datos in json)
            {
                var b = datos.ToString().Trim(new char[] { '[', ']' });
                string[] a = b.Split(',');
                switch (a[0])
                {
                    case "correo":

                        lblmensajecorreo.Text = a[1];
                        break;
                    case "clave":

                        lblmensajeclave.Text = a[1];
                        break;
                    case "cclave":

                        lblmensajecclave.Text = a[1];
                        break;
                    case "nombre":

                        lblmensajenombre.Text = a[1];
                        break;
                    case "apellido":

                        lblmensajeapellido.Text = a[1];
                        break;
                    case "cedula":

                        lblmensajecedula.Text = a[1];
                        break;
                    case "error":
                        await DisplayAlert("Error Registar", a[1], "Okay");

                        break;
                    case "message":
                        string auxUser = eliminarCaracteres(b).Remove(0, 1).Trim();
                        await DisplayAlert("Usuario", "Actualizado ", "OK");
                        await Navigation.PushAsync(new dashboard(cadena));
                        break;

                }

                




            }



        }

        public string eliminarCaracteres(string cadena)
        {
            string[] charsToRemove = new string[] { "{", "}", "user" };
            foreach (var c in charsToRemove)
            {
                cadena = cadena.Replace(c, string.Empty);
            }

            return cadena.Trim();
        }

        public void limpiarErrorCampos()
        {
            lblmensajeclave.Text = "";
            lblmensajecorreo.Text = "";
            lblmensajeapellido.Text = "";
            lblmensajenombre.Text = "";
            lblmensajecclave.Text = "";
            lblmensajecedula.Text = "";

        }

        private async void Btn_inicio(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new dashboard(cadena));
        }

        private async void Btn_agenda(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new Views.Agenda.agenda(cadena));
        }
    }
}