using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoU3Cine.Views;

namespace ProyectoU3Cine
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ListadoPeliculasView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
