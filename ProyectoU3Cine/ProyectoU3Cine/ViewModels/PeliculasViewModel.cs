using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using ProyectoU3Cine.Models;
using ProyectoU3Cine.Views;
using Xamarin.Forms;

namespace ProyectoU3Cine.ViewModels
{
    public class PeliculasViewModel:INotifyPropertyChanged
    {
        int índice;
        public ObservableCollection<Pelicula> ListaPeliculas { get; set; } = new ObservableCollection<Pelicula>();
        public Pelicula Pelicula { get; set; }
        public string MensajeError { get; set; }
        public ICommand CambiarVistaCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand VerDetallesCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public PeliculasViewModel()
        {
            CargarDatos();
            CambiarVistaCommand = new Command<string>(CambiarVista);
            AgregarCommand = new Command(AgregarPelicula);
            EliminarCommand = new Command<Pelicula>(EliminarPelicula);
            VerDetallesCommand = new Command<Pelicula>(DetallesPelicula);
            EditarCommand = new Command<Pelicula>(EditarPelicula);
            GuardarCommand = new Command(Guardar);
        }

        AgregarPeliculaView VistaAgregar;
        EditarPeliculaView VistaEditar;
        DetallesPeliculaView VistaDetalles;


        private void EditarPelicula(Pelicula pe)
        {
            MensajeError = "";
            Property();
            índice = ListaPeliculas.IndexOf(pe);

            Pelicula = new Pelicula
            {
                Nombre = pe.Nombre,
                Sinopsis = pe.Sinopsis,
                Género = pe.Género,
                Duración = pe.Duración,
                Año = pe.Año,
                Calificación = pe.Calificación,
                ImagenURL = pe.ImagenURL
            };

            VistaEditar = new EditarPeliculaView()
            {
                BindingContext = this
            };

            App.Current.MainPage.Navigation.PushAsync(VistaEditar);
        }

        private void Guardar()
        {
            if (Pelicula != null)
            {
                MensajeError = "";

                if (string.IsNullOrWhiteSpace(Pelicula.Nombre))
                {
                    MensajeError = "Escribe el nombre de la película";
                }

                if (string.IsNullOrWhiteSpace(Pelicula.Sinopsis))
                {
                    MensajeError = "Escribe la sinopsis de la película";
                }

                if (string.IsNullOrWhiteSpace(Pelicula.Género))
                {
                    MensajeError = "Escribe el género de la película";
                }

                if (Pelicula.Duración <= 1)
                {
                    MensajeError = "Escribe la duración de la película";
                }

                if (Pelicula.Año <= 0)
                {
                    MensajeError = "Escribe el año en que salió la película";
                }

                else if (Pelicula.Año > 2022)
                {
                    MensajeError = "Debes escribir una película que haya salido antes o durante el 2022";
                }

                if (Pelicula.Calificación < 0 || Pelicula.Calificación > 10)
                {
                    MensajeError = "Escribe la calificación que le das a la película entre un rango de 0 a 10";
                }

                if (!Uri.TryCreate(Pelicula.ImagenURL, UriKind.Absolute, out var uri))
                {
                    MensajeError = "Escribe el URL de la imagen de portada que deseas en la película";
                }

                if (string.IsNullOrWhiteSpace(MensajeError))
                {
                    ListaPeliculas[índice] = Pelicula;
                    GuardarDatos();
                    App.Current.MainPage.Navigation.PopToRootAsync();
                    MensajeError = "";
                }
                Property();
            }

            
        }

        private void DetallesPelicula(Pelicula obj)
        {
            VistaDetalles = new DetallesPeliculaView()
            {
                BindingContext = obj
            };
            App.Current.MainPage.Navigation.PushAsync(VistaDetalles);
        }

        private void EliminarPelicula(Pelicula pe)
        {
            if (pe != null)
            {
                ListaPeliculas.Remove(pe);
                GuardarDatos();
            }
        }

        private void AgregarPelicula()
        {
            if(Pelicula != null)
            {
                MensajeError="";

                if (string.IsNullOrWhiteSpace(Pelicula.Nombre))
                {
                    MensajeError = "Escribe el nombre de la película";
                }

                if(string.IsNullOrWhiteSpace(Pelicula.Sinopsis))
                {
                    MensajeError = "Escribe la sinopsis de la película";
                }

                if(string.IsNullOrWhiteSpace(Pelicula.Género))
                {
                    MensajeError = "Escribe el género de la película";
                }

                if(Pelicula.Duración <= 0)
                {
                    MensajeError = "Escribe la duración de la película";
                }

                if (Pelicula.Año <= 0)
                {
                    MensajeError = "Escribe el año en que salió la película";
                }
                else if(Pelicula.Año > 2022)
                {
                    MensajeError = "Debes escribir una película que haya salido antes o durante el 2022";
                }

                if(Pelicula.Calificación < 0 || Pelicula.Calificación > 10)
                {
                    MensajeError = "Escribe la calificación que le das a la película entre un rango de 0 a 10";
                }

                if(!Uri.TryCreate(Pelicula.ImagenURL, UriKind.Absolute, out var uri))
                {
                    MensajeError = "Escribe el URL de la imagen de portada que deseas en la película";
                }

                if(string.IsNullOrWhiteSpace(MensajeError))
                {
                    ListaPeliculas.Add(Pelicula);
                    CambiarVista("Ver");
                    GuardarDatos();
                }
                Property();
            }
        }

        private void CambiarVista(string vista)
        {
            if(vista == "Agregar")
            {
                Pelicula = new Pelicula();
                VistaAgregar = new AgregarPeliculaView()
                {
                    BindingContext = this
                };
                App.Current.MainPage.Navigation.PushAsync(VistaAgregar);
            }
            else if (vista == "Ver")
            {
                App.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        void GuardarDatos()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "listapeliculas.json";
            File.WriteAllText(file, JsonConvert.SerializeObject(ListaPeliculas));
        }

        void CargarDatos()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "listapeliculas.json";
            if (File.Exists(file))
            {
                ListaPeliculas = JsonConvert.DeserializeObject<ObservableCollection<Pelicula>>(File.ReadAllText(file));
            }
        }

        private void Property()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }


        public event PropertyChangedEventHandler PropertyChanged;


    }
    
}
