using Ej4_Tienda.Models;

namespace Ej4_Tienda.Pages;

public partial class ConsultaLista : ContentPage
{
    private const string TodasLasCiudades = "Todas";

    public ConsultaLista()
    {
        InitializeComponent();
        pickerVip.SelectedIndex = 0; // "Todos"
    }

    // Cada vez que se entra en la pestaña se recargan las ciudades y la lista,
    // por si se han añadido, modificado o borrado clientes en "Altas".
    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarCiudades();
        Filtrar();
    }

    private void CargarCiudades()
    {
        // Recordamos la ciudad elegida para no perderla al recargar
        string? ciudadElegida = pickerCiudad.SelectedItem as string;

        List<string> ciudades = new() { TodasLasCiudades };
        ciudades.AddRange(ServicioClientes.Clientes
            .Select(c => c.ciudad.Trim())
            .Where(c => c != string.Empty)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(c => c));

        pickerCiudad.ItemsSource = ciudades;

        int indice = ciudadElegida == null ? 0 : ciudades.IndexOf(ciudadElegida);
        pickerCiudad.SelectedIndex = indice >= 0 ? indice : 0;
    }

    private void OnFiltroCambiado(object? sender, EventArgs e)
    {
        Filtrar();
    }

    private void Filtrar()
    {
        string ciudad = pickerCiudad.SelectedItem as string ?? TodasLasCiudades;
        string tipo = pickerVip.SelectedItem as string ?? "Todos";

        IEnumerable<Cliente> resultado = ServicioClientes.Clientes;

        if (ciudad != TodasLasCiudades)
            resultado = resultado.Where(c => string.Equals(c.ciudad.Trim(), ciudad, StringComparison.OrdinalIgnoreCase));

        if (tipo == "VIP")
            resultado = resultado.Where(c => c.vip);
        else if (tipo == "No VIP")
            resultado = resultado.Where(c => !c.vip);

        listaClientes.ItemsSource = resultado
            .OrderBy(c => c.apellido)
            .ThenBy(c => c.nombre)
            .ToList();
    }
}
