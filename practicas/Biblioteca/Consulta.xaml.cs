using System.Linq;

namespace Biblioteca;

public partial class Consulta : ContentPage
{
    public Consulta()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        radioAutor.IsChecked = false;
        radioEditorial.IsChecked = false;

        listaAutorEditorial.SelectedItem = null;
        listaAutorEditorial.ItemsSource = null;

        LimpiarResultados();
    }

    private void LimpiarResultados()
    {
        listaTitulo.SelectedItem = null;
        listaTitulo.ItemsSource = null;
        imagenPortadaAlta.Source = null;
    }

    private void TipoConsulta_CheckedChanged(
        object sender,
        CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            CargarOpcionesConsulta();
        }
    }

    private void CargarOpcionesConsulta()
    {
        listaAutorEditorial.SelectedItem = null;
        LimpiarResultados();

        if (radioAutor.IsChecked)
        {
            listaAutorEditorial.ItemsSource = DatosBiblioteca.Libros
                .Select(libro => libro.Autor)
                .Distinct()
                .OrderBy(nombre => nombre)
                .ToList();
        }
        else if (radioEditorial.IsChecked)
        {
            listaAutorEditorial.ItemsSource = DatosBiblioteca.Libros
                .Select(libro => libro.Editorial)
                .Distinct()
                .OrderBy(nombre => nombre)
                .ToList();
        }
    }

    private void listaAutorEditorial_ItemSelected(
        object sender,
        SelectedItemChangedEventArgs e)
    {
        LimpiarResultados();

        if (e.SelectedItem is not string nombreSeleccionado)
        {
            return;
        }

        if (radioAutor.IsChecked)
        {
            listaTitulo.ItemsSource = DatosBiblioteca.Libros
                .Where(libro => libro.Autor == nombreSeleccionado)
                .OrderBy(libro => libro.Titulo)
                .ToList();
        }
        else if (radioEditorial.IsChecked)
        {
            listaTitulo.ItemsSource = DatosBiblioteca.Libros
                .Where(libro => libro.Editorial == nombreSeleccionado)
                .OrderBy(libro => libro.Titulo)
                .ToList();
        }
    }

    private void listaTitulo_ItemSelected(
        object sender,
        SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Libro libroSeleccionado)
        {
            imagenPortadaAlta.Source = libroSeleccionado.Portada;
        }
        else
        {
            imagenPortadaAlta.Source = null;
        }
    }
}
