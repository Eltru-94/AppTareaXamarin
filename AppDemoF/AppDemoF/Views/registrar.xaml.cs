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
    public partial class registrar : ContentPage
    {
        Services.ApiService ModelUser;
        public registrar()
        {
            ModelUser = new Services.ApiService();
            InitializeComponent();
        }

        private  async void store(object sender, EventArgs e)
        {



            string nombre = txtnombre.Text;
            string apellido = txtapellido.Text;
            string correo = txtcorreo.Text;
            string clave = txtpassword.Text;
            string mensaje= await ModelUser.registrarUser(correo, clave, nombre, apellido);
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
                    case "nombre":

                        lblmensajenombre.Text = a[1];
                        break;
                    case "apellido":

                        lblmensajeapellido.Text = a[1];
                        break;
                    case "error":
                        await DisplayAlert("Error Registar", a[1], "Okay");

                        break;
                    case "user":
                        string auxUser = eliminarCaracteres(b).Remove(0, 1).Trim();
                        await DisplayAlert("Usuario Registrado",nombre+" "+apellido, "OK");
                        await Navigation.PushAsync(new dashboard(auxUser));
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
            
        }

    }
}