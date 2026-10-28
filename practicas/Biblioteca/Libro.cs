using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    internal class Libro
    {
        private string? titulo;
        private string? autor;
        private string? editorial;
        private Image? img;

        public Libro(string? titulo, string? autor, string? editorial, Image? img)
        {
            this.titulo = titulo;
            this.autor = autor;
            this.editorial = editorial;
            this.img = img;
        }
        public string? Titulo { get { return titulo; } }
        public string? Autor { get { return autor; } }
        public string? Editorial { get { return editorial; } }
        public Image? Image { get { return img; } }
        public ImageSource? Portada { get { return img?.Source; } }

    }
}
