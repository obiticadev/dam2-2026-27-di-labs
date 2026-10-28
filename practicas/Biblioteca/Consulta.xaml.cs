namespace Biblioteca;

public partial class Consulta : ContentPage
{
	public Consulta()
	{
		InitializeComponent();
	}

    private void listaAutorEditorial_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        string? autorPulsado = e.SelectedItem as string;
        if (autorPulsado != null)
        {
                
        }
    }
    private void listaTitulo_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        Libro? libroPulsado = e.SelectedItem as Libro;
        if (libroPulsado != null)
        {
            imagenPortadaAlta.Source = libroPulsado.Portada;
        }
    }
}
