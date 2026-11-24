using System.Collections.ObjectModel;

namespace Ej4_Tienda.Models
{
    public static class ServicioClientes
    {
        // La lista de clientes, compartida por toda la app
        public static ObservableCollection<Cliente> Clientes { get; } = new();

        // Ruta del fichero dentro de la carpeta AppData de la aplicación
        private static readonly string rutaArchivo =
            Path.Combine(FileSystem.AppDataDirectory, "datosClientes.txt");

        public static string RutaArchivo => rutaArchivo;

        // Lee el fichero y rellena la lista
        public static void Cargar()
        {
            Clientes.Clear();

            if (!File.Exists(rutaArchivo))
                return;

            foreach (string linea in File.ReadAllLines(rutaArchivo))
            {
                string[] datos = linea.Split('#');
                if (datos.Length != 6)
                    continue;

                Clientes.Add(new Cliente
                {
                    nombre = datos[0],
                    apellido = datos[1],
                    ciudad = datos[2],
                    correo = datos[3],
                    comentarios = datos[4],
                    vip = bool.Parse(datos[5])
                });
            }
        }
        public static void Guardar()
        {
            List<string> lineas = new();

            foreach (Cliente c in Clientes)
            {
                lineas.Add($"{c.nombre}#{c.apellido}#{c.ciudad}#{c.correo}#{c.comentarios}#{c.vip}");
            }

            File.WriteAllLines(rutaArchivo, lineas);
        }
    }
}
