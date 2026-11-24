using Ej4_Tienda.Models;

namespace Ej4_Tienda.Pages;

public partial class Altas : ContentPage
{
    private Cliente? clienteSeleccionado;
	public Altas()
	{
		InitializeComponent();
        listaClientes.ItemsSource = ServicioClientes.Clientes;
    }



    private void OnVipLabelTapped(object sender, TappedEventArgs e)
    {
        altaVip.IsChecked = !altaVip.IsChecked;
    }

    private async void OnAnadirClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(altaNombre.Text) || string.IsNullOrWhiteSpace(altaApellido.Text))
        {
            await DisplayAlert("Faltan datos", "El nombre y los apellidos son obligatorios", "OK");
            return;
        }
        Cliente nuevo = new Cliente
        {
            nombre = altaNombre.Text.Trim(),
            apellido = altaApellido.Text.Trim(),
            ciudad = altaCiudad.Text ?? string.Empty,
            correo = altaCorreo.Text ?? string.Empty,
            comentarios = altaComentarios.Text ?? string.Empty,
            vip = altaVip.IsChecked
        };

        ServicioClientes.Clientes.Add(nuevo);

        LimpiarFormulario();


    }

    private void LimpiarFormulario()
    {
        altaNombre.Text = string.Empty;
        altaApellido.Text = string.Empty;
        altaCiudad.Text = string.Empty;
        altaCorreo.Text = string.Empty;
        altaComentarios.Text = string.Empty;
        altaVip.IsChecked = false;

        listaClientes.SelectedItem = null;
    }

    private void OnClienteSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        clienteSeleccionado = listaClientes.SelectedItem as Cliente;
        if (clienteSeleccionado == null)
        {
            return;
        }
        altaNombre.Text = clienteSeleccionado.nombre;
        altaApellido.Text = clienteSeleccionado.apellido;
        altaCiudad.Text = clienteSeleccionado.ciudad;
        altaCorreo.Text = clienteSeleccionado.correo;
        altaComentarios.Text = clienteSeleccionado.comentarios;
        altaVip.IsChecked = clienteSeleccionado.vip;
    }

    private async void OnModificarClicked(object sender, EventArgs e)
    {
        if (clienteSeleccionado == null)
        {
            await DisplayAlert("Modificar", "selecciona primero un cliente de la lista", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(altaNombre.Text) || string.IsNullOrWhiteSpace(altaApellido.Text))
        {
            await DisplayAlert("Faltan datos", "El nombre y los apellidos son obligatorios", "OK");
            return;
        }

        int posicion = ServicioClientes.Clientes.IndexOf(clienteSeleccionado);
        ServicioClientes.Clientes[posicion] = new Cliente
        {
            nombre = altaNombre.Text.Trim(),
            apellido = altaApellido.Text.Trim(),
            ciudad = altaCiudad.Text ?? string.Empty,
            correo = altaCorreo.Text ?? string.Empty,
            comentarios = altaComentarios.Text ?? string.Empty,
            vip = altaVip.IsChecked
        };
        LimpiarFormulario();
    }

    private void OnLimpiarClicked(object sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    private async void OnBorrarClicked(object sender, EventArgs e)
    {
        if (clienteSeleccionado == null)
        {
            await DisplayAlert("Borrar", "Selecciona primero un cliente de la lista", "OK");
            return;
        }

        Cliente aBorrar = clienteSeleccionado;

        bool confirmar = await DisplayAlert("Borrar", $"¿Seguro que quieres borrar a {aBorrar.nombre} {aBorrar.apellido}?", "Si", "No");
        if (!confirmar)
        {
            return;
        }
        ServicioClientes.Clientes.Remove(aBorrar);
        LimpiarFormulario();
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        if (ServicioClientes.Clientes.Count == 0)
        {
            await DisplayAlert("Guardar", "No hay clientes para guardar.", "OK");
            return;
        }

        ServicioClientes.Guardar();
        await DisplayAlert("Guardar",
            $"Clientes guardados correctamente en:\n{ServicioClientes.RutaArchivo}", "OK");
    }
}
