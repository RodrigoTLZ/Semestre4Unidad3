using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoU3Cine.Models
{
    public class Pelicula
    {
        public string Nombre { get; set; } = "";
        public string Sinopsis { get; set; } = "";
        public string Género { get; set; } = "";
        public int Duración { get; set; }
        public int Año { get; set; }
        public float Calificación { get; set; }
        public string ImagenURL { get; set; } = "";

    }
}
