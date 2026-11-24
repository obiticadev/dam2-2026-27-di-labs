using Ej4_Tienda.Models;

namespace Ej4_Tienda.Pages;

public partial class ConsultaIndividualConBusqueda : ContentPage
{
    // Clientes que se están mostrando ahora mismo (ya filtrados)
    private List<Cliente> clientesFiltrados = new();

    public ConsultaIndividualConBusqueda()
    {
        InitializeComponent();
    }

    // Al entrar en la pestaña se vuelve a filtrar, por si han cambiado los clientes en "Altas"
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Filtrar();
    }

    private void OnBusquedaCambiada(object? sender, TextChangedEventArgs e)
    {
        Filtrar();
    }

    private void Filtrar()
    {
        string texto = (entradaBusqueda.Text ?? string.Empty).Trim();

        clientesFiltrados = ServicioClientes.Clientes
            .Where(c => texto == string.Empty
                     || c.nombre.Contains(texto, StringComparison.OrdinalIgnoreCase)
                     || c.apellido.Contains(texto, StringComparison.OrdinalIgnoreCase)
                     || c.ciudad.Contains(texto, StringComparison.OrdinalIgnoreCase)
                     || c.correo.Contains(texto, StringComparison.OrdinalIgnoreCase))
            .OrderBy(c => c.apellido)
            .ThenBy(c => c.nombre)
            .ToList();

        listaClientes.ItemsSource = clientesFiltrados;

        // Recomendación del enunciado: seleccionar siempre el primero por defecto
        Seleccionar(0);
    }

    private void Seleccionar(int indice)
    {
        if (clientesFiltrados.Count == 0)
        {
            listaClientes.SelectedItem = null;
            ActualizarBotones();
            return;
        }

        listaClientes.SelectedItem = clientesFiltrados[indice];
        listaClientes.ScrollTo(indice, position: ScrollToPosition.MakeVisible, animate: true);
        ActualizarBotones();
    }

    private int IndiceActual()
    {
        return listaClientes.SelectedItem is Cliente c ? clientesFiltrados.IndexOf(c) : -1;
    }

    private void ActualizarBotones()
    {
        int indice = IndiceActual();
        botonAnterior.IsEnabled = indice > 0;
        botonSiguiente.IsEnabled = indice >= 0 && indice < clientesFiltrados.Count - 1;
    }

    private void OnAnteriorClicked(object? sender, EventArgs e)
    {
        int indice = IndiceActual();
        if (indice > 0)
            Seleccionar(indice - 1);
    }

    private void OnSiguienteClicked(object? sender, EventArgs e)
    {
        int indice = IndiceActual();
        if (indice < clientesFiltrados.Count - 1)
            Seleccionar(indice + 1);
    }

    // Si el usuario pulsa directamente en un cliente, los botones se actualizan igual
    private void OnSeleccionCambiada(object? sender, SelectionChangedEventArgs e)
    {
        ActualizarBotones();
    }
}
