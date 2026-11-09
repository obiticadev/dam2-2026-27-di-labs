namespace Biblioteca;

public partial class Alta : ContentPage
{
	public Alta()
	{
		InitializeComponent();
	}

    private void LimpiarFormulario()
    {
        titulo.Text = string.Empty;
        autor.Text = string.Empty;
        editorial.Text = string.Empty;
        imagenPortada.Source = null;
    }

    private void Limpiar_Clicked(object sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    private async void Guardar_clicked(object sender, EventArgs e)
    {
        string tituloLibro = titulo.Text?.Trim() ?? string.Empty;
        string autorLibro = autor.Text?.Trim() ?? string.Empty;
        string editorialLibro = editorial.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(tituloLibro) ||
            string.IsNullOrWhiteSpace(autorLibro) ||
            string.IsNullOrWhiteSpace(editorialLibro))
        {
            await DisplayAlert(
                "Faltan datos",
                "Rellena título, autor y editorial",
                "Aceptar"
                );
            return;
        }

        if (imagenPortada.Source == null)
        {
            await DisplayAlert(
                "Falta la portada",
                "Selecciona una portada antes de guardar el libro.",
                "Aceptar");

            return;
        }

        Image portadaLibro = new Image
        {
            Source = imagenPortada.Source
        };

        Libro nuevoLibro = new Libro
        (
            tituloLibro,
            autorLibro,
            editorialLibro,
            portadaLibro
        );

        DatosBiblioteca.Libros.Add(nuevoLibro);

        await DisplayAlert(
            "Libro guardado",
            $"Se ha guardado «{tituloLibro}».",
            "Aceptar");

        LimpiarFormulario();
    }

    private async void SeleccionarImagen_Clicked(object sender, EventArgs e)
    {
        try
        {
            FileResult? archivo = await FilePicker.Default.PickAsync(
                new PickOptions
                {
                    PickerTitle = "Selecciona la portada del libro",
                    FileTypes = FilePickerFileType.Images
                });

            if (archivo == null)
            {
                return;
            }

            using Stream lectura = await archivo.OpenReadAsync();
            using MemoryStream memoria = new MemoryStream();

            await lectura.CopyToAsync(memoria);

            byte[] datosImagen = memoria.ToArray();

            imagenPortada.Source = ImageSource.FromStream(
                () => new MemoryStream(datosImagen));
        }
        catch (Exception)
        {
            await DisplayAlert(
                "Error",
                "No se ha podido cargar la imagen. Prueba con otra.",
                "Aceptar");
        }
    }

}
