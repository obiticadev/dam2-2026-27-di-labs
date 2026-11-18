using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ej4_Tienda.Models
{
    public class Cliente
    {
        public string nombre { get; set; } = string.Empty;
        public string apellido { get; set; } = string.Empty;
        public string ciudad {  get; set; } = string.Empty;
        public string correo {  get; set; } = string.Empty;
        public string comentarios { get; set; } = string.Empty;
        public bool vip {  get; set; }

    }
}
