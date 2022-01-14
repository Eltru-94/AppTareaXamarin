using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDemoF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class login : ContentPage
    {
        Services.ApiService SerData;
        public login()
        {
            SerData = new Services.ApiService();
          
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            String correo = txtemail.Text;
            String password = txtpassword.Text;
           
            var mensaje = await SerData.checkLogin(correo, password);
            
            JObject json = JObject.Parse(mensaje);
            limpiarCampos();
            foreach (var datos in json)
            {
                var b = datos.ToString().Trim(new char[] { '[',']' });
                string[] a = b.Split(',');
                switch (a[0])
                {
                    case "correo":
                        
                        lblmensajecorreo.Text = a[1];
                        break;
                    case "clave":
                       
                        lblmensajeclave.Text = a[1];
                        break;
                    case "error":
                        await DisplayAlert("Error Login", a[1],"Okay", "Cancel");

                        break;
                    case "user":
                        string auxUser = eliminarCaracteres(b).Remove(0,1).Trim();
                       
                        await Navigation.PushAsync(new dashboard(auxUser));
                        break;

                }

                
               
            }

          

            //await DisplayAlert("Login Successfull","", "Okay", "Cancel");

        }

        private void Btn_ViewRegistrar(object sender, EventArgs e)
        {


            Navigation.PushAsync(new Views.registrar());


        }

        public void limpiarCampos()
        {
            lblmensajeclave.Text = "";
            lblmensajecorreo.Text="";
            txtemail.Text = "";
            txtpassword.Text = "";
        }


        public string eliminarCaracteres(string cadena)
        {
            string[] charsToRemove = new string[] { "{", "}", "user"};
            foreach (var c in charsToRemove)
            {
                cadena = cadena.Replace(c, string.Empty);
            }

            return cadena.Trim();
        }

    }
}