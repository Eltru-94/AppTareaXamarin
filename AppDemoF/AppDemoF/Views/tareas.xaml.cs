
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
    public partial class tareas : ContentPage
    {
        string cadena;
        Services.ServiceTask ObjServiceTask;
        string nombre;
        string apellido;
        string correo;
        int id_user;
        public tareas(string cadena)
        {
            InitializeComponent();
            ObjServiceTask = new Services.ServiceTask();
            this.cadena = cadena;
            datos(cadena);
            allTasks();
        }

        private async void Btn_addtarea(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new Views.addTareas(cadena));


        }
        private async void Btn_tareas(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new Views.tareas(cadena));
        }
        private async void Btn_inicio(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new dashboard(cadena));
        }


        public async void allTasks()
        {

            List<Model.Tasks> tasks = await ObjServiceTask.allTaskUser(id_user.ToString());
            listask.ItemsSource = tasks;

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

        private async void Btn_deleteTask(object sender, EventArgs e)
        {

            string id_task = ((Button)sender).BindingContext as string;
            bool answer = await DisplayAlert("Esta seguro?", "Eliminar Tarea", "Si", "No");
            if (answer)
            {
                await ObjServiceTask.deletTask(id_task);
                await Navigation.PushAsync(new Views.tareas(cadena));
            }
        }

        private async void Btn_updateTask(object sender, EventArgs e)
        {

            string id_task = ((Button)sender).BindingContext as string;
            bool answer = await DisplayAlert("Esta seguro?", "Actualizar Tarea", "Si", "No");
            if (answer)
            {
               
               await Navigation.PushAsync(new Views.editTareas(cadena,id_task));
            }
        }
    }
}