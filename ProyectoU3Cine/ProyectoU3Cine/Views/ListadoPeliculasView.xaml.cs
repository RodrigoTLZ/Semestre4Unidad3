using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoU3Cine.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListadoPeliculasView : ContentPage
    {
        public ListadoPeliculasView()
        {
            InitializeComponent();
        }

        private async void SwipeItem_Clicked(object sender, EventArgs e)
        {
            if(await DisplayAlert("Confirmación de eliminación",$"¿Estás seguro que deseas eliminar esta película?","Si","No") == true)
            {
                var sw = (SwipeItem)sender;
                pvm.EliminarCommand.Execute(sw.CommandParameter);
            }
        }
    }
}