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
    public partial class agenda : ContentPage
    {

        string nombre;
        string apellido;
        string correo;
        string cadena;
        int id_user;
        Services.ServiceAgenda ObjServiceAgenda;
        public agenda(string cadena)
        {
            InitializeComponent();
            ObjServiceAgenda = new Services.ServiceAgenda();
            this.cadena = cadena;
            datos(cadena);
            allAgenda();

        }

        private async void Btn_agenda(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new Views.Agenda.agenda(cadena));
        }

        private async void Btn_addagenda(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new Views.Agenda.addAgenda(cadena));
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
            
           
        }

        public async void allAgenda()
        {

            List<Model.Agenda> agenda = await ObjServiceAgenda.allAgendaUser(id_user.ToString());
            listAgenda.ItemsSource = agenda;

        }

        private async void Btn_deleteAgenda(object sender, EventArgs e)
        {

            string id_agenda = ((Button)sender).BindingContext as string;
            bool answer = await DisplayAlert("Esta seguro?", "Eliminar Contacto", "Si", "No");
            if (answer)
            {
                await ObjServiceAgenda.deletAgenda(id_agenda);
                await Navigation.PushAsync(new Views.Agenda.agenda(cadena));
            }
        }
        private async void Btn_updateAgenda(object sender, EventArgs e)
        {

            string id_agenda = ((Button)sender).BindingContext as string;
            bool answer = await DisplayAlert("Esta seguro?", "Actualizar Contacto", "Si", "No");
            if (answer)
            {

                await Navigation.PushAsync(new Views.Agenda.editAgenda(cadena,id_agenda));
            }
        }

    }
}