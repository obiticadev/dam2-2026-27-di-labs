using Ej4_Tienda.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Ej4_Tienda.Pages;

public partial class Altas : ContentPage
{
    private ObservableCollection<Cliente> clientes = new();
    private Cliente? clienteSeleccionado;
	public Altas()
	{
		InitializeComponent();
        listaClientes.ItemsSource = clientes;

        clientes.Add(new Cliente { nombre = "Luis", apellido = "Fernandez Alcaraz" });
        clientes.Add(new Cliente { nombre = "María", apellido = "Huerta Perines" });
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

        clientes.Add(nuevo);

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

        int posicion = clientes.IndexOf(clienteSeleccionado);
        clientes[posicion] = new Cliente
        {
            nombre = altaNombre.Text.Trim(),
            apellido = altaApellido.Text.Trim(),
            ciudad = altaCiudad.Text.Trim(),
            correo = altaCorreo.Text.Trim(),
            comentarios = altaComentarios.Text.Trim(),
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
        clientes.Remove(aBorrar);
        LimpiarFormulario();
    }
}
