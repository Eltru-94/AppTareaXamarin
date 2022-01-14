using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDemoF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class dashboard : ContentPage
    {
        
        Services.ApiService ModelUser;
        string nombre;
        string apellido;
        string correo;
        string cadena;
        int id_user;
        public dashboard(string cadena)
        {
            
            ModelUser = new Services.ApiService();
            this.cadena = cadena;
            InitializeComponent();
            datos(cadena);
            
        }


        public async void alluser()
        {
            //var users = await ModelUser.selectUser();
            //listUser.ItemsSource = users;
        }

        private async void Btn_inicio(object sender, EventArgs e)
        {

            
                await Navigation.PushAsync(new dashboard(cadena));
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
            lblmensajeUser.Text = "Nombre : "+nombre + " " + apellido;
            lblmensajeUseraux.Text = "Correo : "+correo;
        }
    }
}