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



        private async void Btn_close(object sender, EventArgs e)
        {

            bool answer = await DisplayAlert("Esta seguro?", "Cerrar Sesión", "Si", "No");
            if (answer)
            {
                await Navigation.PushAsync(new login());
            }
            
            
        }
        

        private async void Btn_inicio(object sender, EventArgs e)
        {

            
                await Navigation.PushAsync(new dashboard(cadena));
        }
        private async void Btn_tareas(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new Views.tareas(cadena));
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
            string aux = nombre + " " + apellido;
            lblmensajeUser.Text = aux.ToUpper();
            lblmensajeUseraux.Text = correo;
        }
    }
}